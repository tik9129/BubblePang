using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] Text combo;
    [SerializeField] Text score;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Vector3.zero, 0.1f);
    }

    public void Show(int maxCombo, int score)
    {
        gameObject.SetActive(true);
        combo.text = string.Format("{0:D3}", maxCombo);
        this.score.text = string.Format("{0:D8}", score);
    }
}
