using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        ChangeScene("Main"); // at the end
    }



    public static void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public static bool CheckInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
