using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class BubbleShooter : MonoBehaviour
    {
        [SerializeField] private Bubble prefab;

        private Queue<Bubble> queue = new Queue<Bubble>();

        public void Init(int size)
        {
            for (int i = 0; i < size*2; ++i)
            {
                Bubble temp = Instantiate(prefab);
                temp.SetReturn(this);
                temp.transform.SetParent(transform);
                temp.gameObject.SetActive(false);
                queue.Enqueue(temp);
            }
        }

        public void Shoot(Transform start)
        {
            Bubble temp = queue.Dequeue();
            temp.transform.SetParent(start.transform, false);
            temp.gameObject.SetActive(true);
        }

        public void Return(Bubble bubble)
        {
            if (bubble != null)
            {
                bubble.gameObject.SetActive(false);
                bubble.transform.SetParent(transform, false);
                bubble.transform.localPosition = Vector3.zero;
                queue.Enqueue(bubble);
            }
        }
    }
}
