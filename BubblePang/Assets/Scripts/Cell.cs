using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public const float gapX = 15f / 21f;
    public const float gapY = 18f / 21f;

    public void PutBlock(BlockPool pool)
    {
        Block temp = pool.Depool();
        temp.transform.SetParent(transform, false);
    }
}
