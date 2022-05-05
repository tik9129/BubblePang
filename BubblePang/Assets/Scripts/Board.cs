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

        Vector2 pos = new Vector2(-Cell.DIST_X * (size.w / 2) , 0);
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
                temp.PutBlock(pool.Depool(), 0);
                cells[i, j] = temp;
            }
        }
    }

    protected override bool OnCellLink(Offset offset)
    {
        Cell temp = cells[offset.col, offset.row];
        bool flag = true;
        if(linkedCells.Count != 0)
        {
            Cell last = linkedCells.Peek();
            flag = !temp.IsLinked() && last.IsEqualledBlock(temp) && last.IsNeighbor(temp);
        }

        if(flag)
        {
            linkedCells.Push(temp);
        }
        return flag;
    }

    protected override void OnLinkEnd()
    {
        foreach (Cell temp in linkedCells)
        {
            if (linkedCells.Count > 2)
                pool.Enpool(temp.OutBlock());
            temp.Highlight(false);
        }
        linkedCells.Clear();
        DropBlocks();
        FillBlocks();
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

    private void FillBlocks()
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
