using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] FloatVariable readyTime;
    [SerializeField] FloatVariable playTime;
    [SerializeField] float defaultTime;

    private float startTime;
    private bool isGameReady = false;
    private bool isGameStart = false;

    private void OnEnable()
    {
        readyTime.value = 3;
        playTime.value = defaultTime;
    }

    void Update()
    {
        if (isGameReady)
        {
            readyTime.value -= Time.deltaTime;
            if (readyTime.value < -1)
            {
                isGameReady = false;
            }
        }

        if (isGameStart)
        {
            playTime.value -= Time.deltaTime;
            if (playTime.value < 0)
            {
                isGameStart = false;
            }
        }
    }

    public void RunReadyTimer()
    {
        startTime = Time.time;
        isGameReady = true;
    }

    public void RunPlayTimer()
    {
        startTime = Time.time;
        isGameStart = true;
    }

    public bool IsGameStart()
    {
        return readyTime.value < 0;
    }

    public bool IsTimeOver()
    {
        return playTime.value < 0;
    }
}
