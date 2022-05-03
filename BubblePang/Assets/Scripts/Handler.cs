using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    private Handler next;

    public void SetNext(Handler handler)
    {
        next = handler;
    }

    protected virtual void OnCellLink(Offset offset)
    {
        next.OnCellLink(offset);
    }

    protected virtual void OnLinkEnd()
    {
        next.OnLinkEnd();
    }
}
