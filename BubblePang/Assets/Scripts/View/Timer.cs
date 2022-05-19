using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] FloatVariable time;
        [SerializeField] Text clock;
        [SerializeField] RectTransform gauge;

        private float baseWidth;
        private float playTime;

        private void Awake()
        {
            baseWidth = gauge.rect.width;
            playTime = time.value;
        }

        private void Update()
        {
            clock.text = string.Format("{0:D2}", (int)Mathf.Ceil(time.value));

            float w = baseWidth * time.value / playTime;
            float h = gauge.rect.height;
            gauge.sizeDelta = new Vector2(w,h);
        }
    }
}
