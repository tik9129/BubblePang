using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private GameManager manager;
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private BlockPool pool;
        [SerializeField] private BubbleShooter shooter;
        [SerializeField] private LineRenderer lineRenderer;

        [Space]
        [SerializeField] private Vector2Int size;
        [Space]
        [SerializeField] private UnityEvent OnEndLink;
        

        private Cell[,] cells;
        private Stack<Cell> linkedCells;

        private void Awake()
        {
            cells = new Cell[size.x, size.y];
            linkedCells = new Stack<Cell>();

            Vector2 pos = new Vector2(-Cell.DIST_X * (size.x / 2), 0);
            for (int i = 0; i < size.x; i++)
            {
                pos.y = -(Cell.DIST_Y / 2) * (i & 1);
                for (int j = 0; j < size.y; j++)
                {
                    Cell temp = Instantiate(cellPrefab, transform, true);
                    temp.offset = new Offset(i, j);
                    temp.SetBoard(this);
                    temp.name = temp.offset.ToString();
                    temp.transform.localPosition = pos + new Vector2(Cell.DIST_X * i, Cell.DIST_Y * j);
                    cells[i, j] = temp;
                }
            }
            SetFreeze(true);

            pool.Init(size.x * size.y);
            shooter.Init(size.x * size.y);
        }

        public void SetFreeze(bool flag)
        {
            foreach (Cell temp in cells)
            {
                temp.Highlight(flag);
            }
        }

        public bool LinkCell(Offset offset)
        {
            Cell temp = cells[offset.col, offset.row];
            bool flag = true;
            if (linkedCells.Count != 0)
            {
                Cell last = linkedCells.Peek();
                flag = !temp.IsLinked() && last.IsEqualledBlock(temp) && last.IsNeighbor(temp);
            }

            if (flag)
            {
                linkedCells.Push(temp);
                lineRenderer.positionCount = linkedCells.Count;
                lineRenderer.SetPosition(linkedCells.Count - 1, temp.transform.localPosition);
            }
            return flag;
        }

        public void EndLink()
        {
            lineRenderer.positionCount = 0;
            if (linkedCells.Count > 2)
                OnEndLink.Invoke();
            foreach (Cell temp in linkedCells)
            {
                if (linkedCells.Count > 2 || temp.GetBlockIndex() >= 4)
                {
                    temp.BreakBlock();
                    shooter.Shoot(temp.transform);
                }
                temp.Highlight(false);
            }
            if (linkedCells.Count > 7)
            {
                pool.CreateItem();
                linkedCells.Peek().PutBlock(pool.Depool(), 0);
            }

            DropBlocks();
            FillBlocks();
            linkedCells.Clear();
        }

        public void BreakSameBlock()
        {
            int random = Random.Range(0, 4);
            foreach (Cell temp in cells)
            {
                if (!temp.IsEmpty() && temp.GetBlockIndex() == random)
                {
                    temp.BreakBlock();
                    shooter.Shoot(temp.transform);
                }
            }
        }

        public void ExplodeBlock(Offset offset)
        {
            Vector3[] target = {
                new Vector3(+1, 0, -1), new Vector3(+1, -1, 0), new Vector3(0, -1, +1),
                new Vector3(-1, 0, +1), new Vector3(-1, +1, 0), new Vector3(0, +1, -1),
                new Vector3(+2, 0, -2), new Vector3(+2, -2, 0), new Vector3(0, -2, +2),
                new Vector3(-2, 0, +2), new Vector3(-2, +2, 0), new Vector3(0, +2, -2),
                new Vector3(+2, -1, -1), new Vector3(+1, -2, +1), new Vector3(-1, -1, +2),
                new Vector3(-2, +1, +1), new Vector3(-1, +2, -1), new Vector3(+1, +1, -2)
            };

            for (int i = 0; i < target.Length; ++i)
            {
                Offset temp = new Offset(offset.ToVector3() + target[i]);
                if (0 <= temp.col && temp.col < size.x && 0 <= temp.row && temp.row < size.y)
                {
                    Cell cell = cells[temp.col, temp.row];
                    cell.BreakBlock();
                    shooter.Shoot(cell.transform);
                }
            }

            DropBlocks();
            FillBlocks();
        }

        private void DropBlocks()
        {
            for (int i = 0; i < size.x; i++)
            {
                int num = 0;
                for (int j = 0; j < size.y; j++)
                {
                    Cell temp = cells[i, j];
                    if (temp.IsEmpty()) ++num;
                    else
                    {
                        cells[i, j - num].PutBlock(temp);
                    }
                }
            }
        }

        public void FillBlocks()
        {
            for (int i = 0; i < size.x; i++)
            {
                int num = 0;
                for (int j = 0; j < size.y; j++)
                {
                    Cell temp = cells[i, j];
                    if (temp.IsEmpty())
                    {
                        temp.PutBlock(pool.Depool(), size.y - j + num++);
                    }
                }
            }
        }
    }
}