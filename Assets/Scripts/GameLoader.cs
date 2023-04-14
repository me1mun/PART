using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        GameManager.ChangeScene("Main"); // at the end
    }
}
