using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Offset
{
    public int row, col;

    public Offset(int r, int c)
    {
        row = r;
        col = c;
    }
}
