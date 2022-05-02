using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardSize size;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private BlockPool pool;

    private Cell[,] cells;

    private void Awake()
    {
        cells = new Cell[size.w, size.h];

        Vector2 pos = new Vector2(-Cell.gapX * (size.w / 2) , 0);
        for (int i = 0; i < size.w; i++)
        {
            pos.y = -(Cell.gapY / 2) * (i & 1);
            for (int j = 0; j < size.h; j++)
            {
                Cell temp = Instantiate(cellPrefab, transform, true);
                //temp.offset = new Vector2(i, j);
                //temp.SetNext(this);
                temp.name = i + ", " + j;
                temp.transform.localPosition = pos + new Vector2(Cell.gapX * i, Cell.gapY * j);
                temp.PutBlock(pool);
                cells[i, j] = temp;
            }
        }
    }
}
