using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int level = 0;
    public static int levelsUnlocked = 21;
    public static int levelCount = 83;

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


        //ChangeScene("Main"); // at the end
    }

    public static void ChangeLevel(int count)
    {
        level += count;
        level = Mathf.Clamp(level, 0, 1000);

        Debug.Log(level);
    }

    public static void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
