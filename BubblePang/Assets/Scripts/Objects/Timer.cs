using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] ScriptableFloat readyTime;
        [SerializeField] ScriptableFloat playTime;
        [SerializeField] float defaultTime;

        private float comboTime = 0;
        private bool isConnetedCombo = false;
        private bool isGameReady = false;
        private bool isGameStart = false;

        private void OnEnable()
        {
            readyTime.value = 3;
            playTime.value = defaultTime;
        }

        void Update()
        {
            if (isConnetedCombo)
            {
                comboTime -= Time.deltaTime;
                if (comboTime <= 0)
                {
                    isConnetedCombo = false;
                }
            }

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

        public void ResetComboTime()
        {
            isConnetedCombo = true;
            comboTime = 3;
        }

        public void RunReadyTimer()
        {
            isGameReady = true;
        }

        public void RunPlayTimer()
        {
            isGameStart = true;
        }

        public bool IsConnectingCombo()
        {
            return comboTime > 0;
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
}