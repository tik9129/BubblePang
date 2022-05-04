using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Offset
{
    public int col, row;

    public Offset(int c, int r)
    {
        col = c;
        row = r;
    }

    public override string ToString()
    {
        return col + ", " + row;
    }

    public Vector3 ToVector3()
    {
        float q = col;
        float r = row - (col + (col & 1)) / 2;
        return new Vector3(q, r, -q - r);
    }
}
