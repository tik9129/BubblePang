using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bubble : Handler
{
    [SerializeField] Animator animator;

    [SerializeField] private UnityEvent BubblePang;
    private bool isPopped = false;
    private Vector3 destination;

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, 0.2f);
        if (transform.position == destination && !isPopped)
        {
            isPopped = true;
            animator.SetTrigger("Pop");
            BubblePang.Invoke();
        }
    }

    public void Shoot()
    {
        destination = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(3f, 4f));
        gameObject.SetActive(true);
    }

    public void Return()
    {
        isPopped = false;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
