using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : Handler
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    public int index { get; set; }

    private void OnEnable()
    {
        spriteRenderer.sprite = sprites[index];
    }

    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0), 0.3f);
    }

}
