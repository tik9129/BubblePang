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

        Vector2 pos = new Vector2(-Cell.GAP_X * (size.w / 2) , 0);
        for (int i = 0; i < size.w; i++)
        {
            pos.y = -(Cell.GAP_Y / 2) * (i & 1);
            for (int j = 0; j < size.h; j++)
            {
                Cell temp = Instantiate(cellPrefab, transform, true);
                temp.offset = new Offset(i, j);
                temp.SetNext(this);
                temp.name = i + ", " + j;
                temp.transform.localPosition = pos + new Vector2(Cell.GAP_X * i, Cell.GAP_Y * j);
                temp.PutBlock(pool.Depool());
                cells[i, j] = temp;
            }
        }
    }

    protected override void OnCellLink(Offset offset)
    {
        linkedCells.Push(cells[offset.row, offset.col]);
        Debug.Log(linkedCells.Count);
    }

    protected override void OnLinkEnd()
    {
        while(linkedCells.Count != 0)
        {
            linkedCells.Pop().SetLink(false);
        }
    }
}
