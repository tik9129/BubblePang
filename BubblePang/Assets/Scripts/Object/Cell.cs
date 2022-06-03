using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    public class Cell : MonoBehaviour
    {
        public const float DIST_X = 15f / 21f;
        public const float DIST_Y = 18f / 21f;

        [Space]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite idleSprite;
        [SerializeField] private Sprite highlightedSprite;
        [Space]
        [SerializeField] private Collider2D targetCollider;

        private Board board;
        private Block block;
        private bool isLinked = false;

        public Offset offset { get; set; }

        public int GetBlockIndex()
        {
            return block.index;
        }

        public void SetBoard(Board board)
        {
            this.board = board;
        }

        public void PutBlock(Block block, int blockDrop)
        {
            this.block = block;
            block.transform.SetParent(transform, false);
            if (blockDrop != 0)
            {
                block.transform.position += new Vector3(0, DIST_Y * blockDrop);
            }
        }

        public void PutBlock(Cell cell)
        {
            block = cell.OutBlock();
            block.transform.SetParent(transform);
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
            spriteRenderer.sortingOrder = flag ? 3 : 1;
            targetCollider.enabled = !flag;
        }

        public void SetLink()
        {
            bool flag = board.LinkCell(offset);
            if (flag)
            {
                Highlight(true);
            }
        }

        public void EndLink()
        {
            board.EndLink();
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

        public void BreakBlock()
        {
            if (block != null)
            {
                Block temp = OutBlock();
                temp.Break();

                switch(temp.index)
                {
                    case 4:
                        board.ExplodeBlock(offset);
                        break;
                    case 5:
                        board.BreakSameBlock();
                        break;
                }
            }
        }
    }

}