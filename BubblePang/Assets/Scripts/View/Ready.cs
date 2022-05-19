using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Ready : MonoBehaviour
    {
        [SerializeField] private FloatVariable readyTime;
        [SerializeField] private Image targetImage;
        [SerializeField] private Sprite[] sprite;

        private int nowNum = 3;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, 0.2f);
            if (Mathf.Ceil(readyTime.value) == -1)
            {
                gameObject.SetActive(false);
            }
            else if (Mathf.Ceil(readyTime.value) < nowNum)
            {
                --nowNum;
                targetImage.sprite = sprite[nowNum];
                transform.position = new Vector3(-5, 0, 0);
            }
        }
    }
}
