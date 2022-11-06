using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Combo : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] ScriptableFloat combo;

        void Update()
        {
            if (combo.value != 0)
            {
                text.text = string.Format("{0:D3} Combo", (int)Mathf.Ceil(combo.value));
            }
            else if (gameObject.activeSelf && combo.value == 0)
            {
                text.text = string.Format("", (int)Mathf.Ceil(combo.value));
            }
        }
    }
}
