using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class BlockPool : MonoBehaviour
    {
        [SerializeField] private Block prefab;

        private Queue<Block> queue = new Queue<Block>();

        public void Init(int size)
        {
            for (int i = 0; i < size*2; ++i)
            {
                Block temp = Instantiate(prefab);
                temp.SetReturn(this);
                Enpool(temp);
            }
        }

        public void Enpool(Block block)
        {
            if (block != null)
            {
                block.transform.SetParent(transform, false);
                block.gameObject.SetActive(false);
                block.index = Random.Range(0, 4);
                queue.Enqueue(block);
            }
        }

        public Block Depool()
        {
            Block temp = queue.Peek();
            temp.gameObject.SetActive(true);
            return queue.Dequeue();
        }

        public void CreateItem()
        {
            Block temp = queue.Peek();
            temp.index = Random.Range(4, 6);
        }
    }
}
