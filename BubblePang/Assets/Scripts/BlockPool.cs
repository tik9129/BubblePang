using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField] private BoardSize size;
    [Space]
    [SerializeField] private Block blockPrefab;

    private Queue<Block> queue;

    private void Awake()
    {
        queue = new Queue<Block>();
        for (int i = 0; i < size.w * size.h; ++i)
        {
            Block temp = Instantiate(blockPrefab);
            Enpool(temp);
        }
    }

    public void Enpool(Block block)
    {
        if(block != null)
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
