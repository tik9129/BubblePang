using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField] private BoardSize size;
    [Space]
    [SerializeField] private Block blockPrefab;
    [SerializeField] private Sprite[] blockSprites;

    private Queue<Block> queue;

    private void Awake()
    {
        queue = new Queue<Block>();
        for (int i = 0; i < size.w * size.h; ++i)
        {
            Block temp = Instantiate(blockPrefab, transform);
            temp.gameObject.SetActive(false);
            queue.Enqueue(temp);
        }
    }

    public void Enpool(Block block)
    {
        block.transform.SetParent(transform, false);
        block.gameObject.SetActive(false);
        queue.Enqueue(block);
    }

    public Block Depool()
    {
        Block temp = queue.Peek();
        temp.gameObject.SetActive(true);
        temp.index = Random.Range(0, 4);
        temp.ChangeSprite(blockSprites[temp.index]);
        return queue.Dequeue();
    }
}
