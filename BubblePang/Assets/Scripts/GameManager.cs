using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
