using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] FloatVariable time;
    [SerializeField] float playTime;
    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
        time.value = playTime;
    }

    void Update()
    {
        if(time.value > 0)
        {
            time.value = playTime - (Time.time - startTime);
        }
        else
        {
            time.value = 0;
        }
    }

    public bool IsTimeOver()
    {
        return time.value == 0;
    }
}
