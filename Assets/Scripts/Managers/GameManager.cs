using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int level = 0;
    public static int levelLast = 1; // levelLast + 1 = real last

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 60;
    }


    public static void ChangeLevel(int count)
    {
        level += count;
        level = Mathf.Clamp(level, 0, LevelData.levelsCount - 1);

        Debug.Log(level);
    }

    public static void SceneChange(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
