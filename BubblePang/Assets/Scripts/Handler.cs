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

    protected virtual bool OnCellLink(Offset offset)
    {
        return next.OnCellLink(offset);
    }

    protected virtual void OnLinkEnd()
    {
        next.OnLinkEnd();
    }

    protected virtual void OnItemUsed(Offset offset, int index)
    {
        next.OnItemUsed(offset, index);
    }
}
