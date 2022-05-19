using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private float delta = 2/21f;
    private bool isEnable = false;

    private void OnEnable()
    {
        isEnable = true;
        StartCoroutine(UpDown());
    }

    private void OnDisable()
    {
        isEnable = false;
    }

    IEnumerator UpDown()
    {
        while(isEnable)
        {
            transform.position += new Vector3(0, delta);
            delta *= -1;
            yield return new WaitForSeconds(1);
        }
    }
}
