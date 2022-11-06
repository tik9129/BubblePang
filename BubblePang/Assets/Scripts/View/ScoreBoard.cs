using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ScoreBoard : MonoBehaviour
    {
        [SerializeField] ScriptableFloat score;
        [SerializeField] Text text;

        private void Update()
        {
            text.text = string.Format("{0:D8}", (int)score.value);
        }
    }
}
