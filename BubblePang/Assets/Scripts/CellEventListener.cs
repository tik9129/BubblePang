using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEventListener : MonoBehaviour
{
    [SerializeField] private Cell targetCell;

    private static bool isSlid = false;

    private void OnMouseEnter()
    {
        if (isSlid)
        {
            targetCell.SetLink();
        }
    }

    private void OnMouseDown()
    {
        isSlid = true;
        targetCell.SetLink();
    }

    private void OnMouseUp()
    {
        isSlid = false;
        targetCell.EndLink();
    }
}
