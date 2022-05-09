using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Handler
{
    [SerializeField] Animator animator;

    private bool isPopped;
    private Vector3 destination;

    private void Awake()
    {
        isPopped = false;
        destination = Vector3.zero;
    }

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, 0.15f);
        if(transform.position == destination && !isPopped)
        {
            isPopped = true;
            animator.SetTrigger("Pop");
            OnBubblePang();
        }
    }

    public void Shoot(Vector3 dest)
    {
        gameObject.SetActive(true);
        destination = dest;
    }

    public void Return()
    {
        isPopped = false;
        destination = Vector3.zero;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
