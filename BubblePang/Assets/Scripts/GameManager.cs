using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;
using UnityEngine.Events;

public class GameManager : Handler
{
    [SerializeField] Window window;
    [SerializeField] Board board;
    [SerializeField] Kraken kraken;
    [SerializeField] FloatVariable score;
    [SerializeField] Timer timer;

    public enum GameState { INIT, TITLE, START, READY, PLAY, END };
    private GameState state;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        board.SetNext(this);
        score.value = 0;
        state = GameState.INIT;
    }

    public void SetState(GameState state)
    {
        this.state = state;
    }

    private void Update()
    {
        switch(state)
        {
            case GameState.INIT:
                window.ShowTitleFrame();
                state = GameState.TITLE;
                break;
            case GameState.TITLE:
                break;
            case GameState.START:
                board.FillBlocks();
                timer.RunReadyTimer();
                state = GameState.READY;
                break;
            case GameState.READY:
                if (timer.IsGameStart())
                {
                    board.SetFreeze(false);
                    timer.RunPlayTimer();
                    state = GameState.PLAY;
                }
                break;
            case GameState.PLAY:
                if (timer.IsTimeOver())
                {
                    kraken.Exit();
                    board.EndLink();
                    board.SetFreeze(true);
                    state = GameState.END;
                }
                break;
        }
    }

    public void AddScore(int num)
    {
        score.value += num;
    }
}
