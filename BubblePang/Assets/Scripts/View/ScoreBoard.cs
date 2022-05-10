using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ScoreBoard : MonoBehaviour
    {
        [SerializeField] FloatVariable score;
        [SerializeField] Text text;

        private void Update()
        {
            text.text = score.value.ToString();
        }
    }
}
