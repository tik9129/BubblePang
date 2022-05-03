using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Handler
{
    public const float GAP_X = 15f / 21f;
    public const float GAP_Y = 18f / 21f;

    [Space]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite highlightedSprite;

    public Offset offset { get; set; }

    private bool isLinked = false;

    public void PutBlock(Block block)
    {
        block.transform.SetParent(transform, false);
    }
    
    public void SetLink(bool flag)
    {
        isLinked = flag;
        spriteRenderer.sprite = flag ? highlightedSprite : idleSprite;
        spriteRenderer.sortingOrder = flag ? 2 : 0;
        if (flag)
            OnCellLink(offset);

    }

    public void EndLink()
    {
        OnLinkEnd();
    }
}
