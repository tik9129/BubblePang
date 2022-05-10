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

        private void Awake()
        {
            baseWidth = gauge.rect.width;
        }

        private void Update()
        {
            clock.text = Mathf.Floor(time.value)+"";
            float w = baseWidth * time.value / 90;
            float h = gauge.rect.height;
            gauge.sizeDelta = new Vector2(w,h);
        }
    }
}
