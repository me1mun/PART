using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int level = 0;
    public static int levelsUnlocked = 1;
    public static int levelCount;

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
        levelCount = 20;// LevelList.Instance.levelJson.Count;

        ChangeScene("Main"); // at the end
    }

    public static void SetLevel(int newLevel)
    {
        //Debug.Log("before " + level);
        if (newLevel > 0 && newLevel < levelsUnlocked)
        {
            level = Mathf.Clamp(newLevel, 0, levelCount);
        }
        //Debug.Log("after " + level);
    }

    public static void UnlockLevel()
    {
        
        if (level >= levelsUnlocked -1 && levelsUnlocked < levelCount)
        {
            levelsUnlocked += 1;
        }
    }

    public static void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
