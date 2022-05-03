using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Block index { get; set; }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
