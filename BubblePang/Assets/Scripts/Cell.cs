using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Handler
{
    public const float DIST_X = 15f / 21f;
    public const float DIST_Y = 18f / 21f;

    [Space]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite highlightedSprite;

    public Offset offset { get; set; }

    private Block block;
    private bool isLinked = false;

    public void PutBlock(Block block)
    {
        block.transform.SetParent(transform, false);
        this.block = block;
    }

    public void PutBlock(Cell cell)
    {
        block = cell.OutBlock();
        block.transform.SetParent(transform,false);
    }

    public Block OutBlock()
    {
        Block temp = block;
        block = null;
        return temp;
    }

    public bool IsEmpty()
    {
        return block == null;
    }

    public void Highlight(bool flag)
    {
        isLinked = flag;
        spriteRenderer.sprite = flag ? highlightedSprite : idleSprite;
        spriteRenderer.sortingOrder = flag ? 2 : 0;
    }
    
    public void SetLink()
    {
        bool flag = OnCellLink(offset);
        if (flag)
        {
            Highlight(true);
        }
    }

    public void EndLink()
    {
        OnLinkEnd();
    }

    public bool IsLinked()
    {
        return isLinked;
    }

    public bool IsEqualledBlock(Cell cell)
    {
        if (this.block == null || cell.block == null)
            return false;
        else
            return this.block.index == cell.block.index;
    }

    public bool IsNeighbor(Cell cell)
    {
        Vector3 delta = cell.offset.ToVector3() - offset.ToVector3();
        Vector3 abs = new Vector3(Mathf.Abs(delta.x), Mathf.Abs(delta.y), Mathf.Abs(delta.z));
        return Mathf.Max(abs.x, abs.y, abs.z) < 2;
    }
}
