using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] private UnityEvent BubblePang;

        private BubbleShooter shooter;
        private bool isPopped = false;
        private Vector3 destination;

        public void SetReturn(BubbleShooter shooter)
        {
            this.shooter = shooter;
        }

        private void OnEnable()
        {
            destination = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(3f, 4f));
        }

        void Update()
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, 0.2f);
            if (transform.position == destination && !isPopped)
            {
                //SoundManager.Instance.PlaySFX(SoundManager.SFX.PANG);
                isPopped = true;
                animator.SetTrigger("Pop");
                BubblePang.Invoke();
            }
        }

        public void Shoot()
        {
            gameObject.SetActive(true);
        }

        public void Return()
        {
            isPopped = false;
            shooter.Return(this);
        }
    }
}
