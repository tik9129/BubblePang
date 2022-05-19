using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
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
        Vector3 move = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0), 0.3f);
        transform.localPosition = move;
    }
}
