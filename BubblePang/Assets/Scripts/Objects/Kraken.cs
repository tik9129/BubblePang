using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Kraken : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void Hit()
        {
            animator.SetTrigger("Hit");
        }

        public void Exit()
        {
            animator.SetTrigger("Exit");
        }
    }
}
