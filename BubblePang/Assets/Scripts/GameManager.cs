using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object;
using View;

public class GameManager : MonoBehaviour
{
    [SerializeField] Window window;
    [SerializeField] Board board;
    [SerializeField] Kraken kraken;
    [SerializeField] FloatVariable score;
    [SerializeField] FloatVariable combo;
    [SerializeField] Timer timer;

    private int maxCombo = 0;

    public enum GameState { INIT, TITLE, START, READY, PLAY, RESULT, END };
    public GameState state { get; set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        score.value = 0;
        combo.value = 0;
        state = GameState.INIT;
    }

    private void Update()
    {
        switch (state)
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
                if (!timer.IsConnectingCombo())
                {
                    combo.value = 0;
                }
                if (timer.IsTimeOver())
                {
                    SoundManager.Instance.OutBGM();
                    kraken.Exit();
                    board.EndLink();
                    board.SetFreeze(true);
                    state = GameState.RESULT;
                }
                break;
            case GameState.RESULT:
                StartCoroutine(ShowResult());
                state = GameState.END;
                break;
            case GameState.END:
                break;
        }
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(3);
        combo.value = 0;
        window.ShowResult(maxCombo, (int)score.value);
    }

    public void AddScore(int num)
    {
        AddCombo();
        score.value += num + combo.value * combo.value;
    }

    public void AddCombo()
    {
        ++combo.value;
        if (maxCombo < combo.value)
            maxCombo = (int)combo.value;
        timer.ResetComboTime();
    }
}