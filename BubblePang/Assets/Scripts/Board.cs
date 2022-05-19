using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Handler
{
    [SerializeField] private BoardSize size;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private BlockPool pool;

    private Cell[,] cells;
    private Stack<Cell> linkedCells;

    private void Awake()
    {
        cells = new Cell[size.w, size.h];
        linkedCells = new Stack<Cell>();

        Vector2 pos = new Vector2(-Cell.DIST_X * (size.w / 2), 0);
        for (int i = 0; i < size.w; i++)
        {
            pos.y = -(Cell.DIST_Y / 2) * (i & 1);
            for (int j = 0; j < size.h; j++)
            {
                Cell temp = Instantiate(cellPrefab, transform, true);
                temp.offset = new Offset(i, j);
                temp.SetNext(this);
                temp.name = temp.offset.ToString();
                temp.transform.localPosition = pos + new Vector2(Cell.DIST_X * i, Cell.DIST_Y * j);
                //temp.PutBlock(pool.Depool(), 0);
                cells[i, j] = temp;
            }
        }
        SetFreeze(true);
    }

    public void SetFreeze(bool flag)
    {
        foreach (Cell temp in cells)
        {
            temp.Highlight(flag);
        }
    }

    protected override bool OnCellLink(Offset offset)
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
        }
        return flag;
    }

    protected override void OnLinkEnd()
    {
        EndLink();
    }

    public void EndLink()
    {
        foreach (Cell temp in linkedCells)
        {
            if (linkedCells.Count > 2)
            {
                temp.BreakBlock();
                pool.Enpool(temp.OutBlock());

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

    protected override void OnItemUsed(Offset offset, int index)
    {
        pool.Enpool(cells[offset.col, offset.row].OutBlock());

        if (index == 5)
        {
            BreakSameBlock();
        }
        else
        {
            ExplodeBlock(offset);
        }

        DropBlocks();
        FillBlocks();
    }

    private void BreakSameBlock()
    {
        int random = Random.Range(0, 4);
        foreach (Cell temp in cells)
        {
            if (!temp.IsEmpty() && temp.GetBlockIndex() == random)
            {
                temp.BreakBlock();
                pool.Enpool(temp.OutBlock());
            }
        }
    }

    private void ExplodeBlock(Offset offset)
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
            if (0 <= temp.col && temp.col < size.w && 0 <= temp.row && temp.row < size.h)
            {
                Cell cell = cells[temp.col, temp.row];
                cell.BreakBlock();
                pool.Enpool(cell.OutBlock());
            }
        }
    }

    private void DropBlocks()
    {
        for (int i = 0; i < size.w; i++)
        {
            int num = 0;
            for (int j = 0; j < size.h; j++)
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
        for (int i = 0; i < size.w; i++)
        {
            int num = 0;
            for (int j = 0; j < size.h; j++)
            {
                Cell temp = cells[i, j];
                if (temp.IsEmpty())
                {
                    temp.PutBlock(pool.Depool(), size.h - j + num++);
                }
            }
        }
    }
}
