using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class Window : MonoBehaviour
    {
        [SerializeField] GameManager manager;
        [Space]
        [SerializeField] GameObject mainCamera;
        [SerializeField] Frame[] frames;
        [SerializeField] GameObject ui;

        public void ShowTitleFrame()
        {
            frames[0].gameObject.SetActive(true);
        }

        public void ShowPlayFrame()
        {
            frames[1].gameObject.SetActive(true);
            StartCoroutine(MoveToPlayFrame(new Vector3(0, 0, -10)));
        }

        IEnumerator MoveToPlayFrame(Vector3 dest)
        {
            Transform temp = mainCamera.transform;
            while (temp.position != dest)
            {
                temp.position = Vector3.MoveTowards(temp.position, dest, 0.3f);
                yield return null;
            }
            ui.SetActive(true);
            frames[0].gameObject.SetActive(false);
            manager.SetState(GameManager.GameState.START);
        }
    }
}
