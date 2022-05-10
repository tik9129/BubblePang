using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] FloatVariable time;
    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
        time.value = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if(time.value > 0)
        {
            time.value = 90 - (Time.time - startTime);
        }
        else
        {
            time.value = 0;
        }

    }
}
