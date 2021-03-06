using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object.EventListener
{
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
            if (!targetCell.IsLinked())
            {
                isSlid = true;
                targetCell.SetLink();
            }
        }

        private void OnMouseUp()
        {
            isSlid = false;
            targetCell.EndLink();
        }
    }
}
