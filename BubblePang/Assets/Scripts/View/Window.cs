using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class Window : MonoBehaviour
    {
        [SerializeField] GameManager manager;
        [Space]
        [SerializeField] GameObject mainCamera;
        [SerializeField] Frame[] frames;
        [SerializeField] GameObject ui;
        [SerializeField] Result result;

        public void ShowTitleFrame()
        {
            frames[0].gameObject.SetActive(true);
        }

        public void ShowPlayFrame()
        {
            frames[1].gameObject.SetActive(true);
            StartCoroutine(MoveToPlayFrame());
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        IEnumerator MoveToPlayFrame()
        {
            yield return new WaitForSeconds(0.3f);

            Transform temp = mainCamera.transform;
            Vector3 dest = new Vector3(0, 0, -10);
            while (temp.position != dest)
            {
                temp.position = Vector3.MoveTowards(temp.position, dest, 0.3f);
                yield return null;
            }
            ui.SetActive(true);
            frames[0].gameObject.SetActive(false);
            manager.state = GameManager.GameState.START;
        }

        public void ShowResult(int maxCombo, int score)
        {
            result.Show(maxCombo, score);
        }

        public void ShowHome()
        {
            SceneManager.LoadScene(0);
        }
    }
}
