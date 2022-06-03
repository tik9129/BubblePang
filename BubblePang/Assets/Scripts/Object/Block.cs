using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Object
{
    public class Block : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] Sprite[] sprites;
        [SerializeField] SpriteRenderer spriteRenderer;

        private BlockPool pool;
        public int index { set; get; }

        public void SetReturn(BlockPool pool)
        {
            this.pool = pool;
        }

        private void OnEnable()
        {
            spriteRenderer.sprite = sprites[index];
        }

        private void Update()
        {
            Vector3 move = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0), 0.3f);
            transform.localPosition = move;
        }

        public void Break()
        {
            animator.enabled = true;
        }

        public void Return()
        {
            spriteRenderer.sprite = sprites[index];
            animator.enabled = false;
            pool.Enpool(this);
        }
    }
}
