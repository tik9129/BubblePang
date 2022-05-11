using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Handler
{
    [SerializeField] Board board;
    [SerializeField] FloatVariable score;
    [SerializeField] Timer timer;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        board.SetNext(this);
        score.value = 0;
    }

    private void Update()
    {
        if(timer.IsTimeOver())
        {
            board.Freeze();
            board.End();
        }
    }

    protected override void OnBubblePang()
    {
        score.value += 50;
    }
}
