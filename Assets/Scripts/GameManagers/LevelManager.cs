using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }


    public int levelsUnlocked = 1;

    public string levelSavePath;

    [SerializeField] private List<TextAsset> levels = new List<TextAsset>();

    public Level levelEmpty = new Level() { width = 8, height = 10 };
    public Level levelRandom = new Level() { isRandom = true };

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

        levelSavePath = Application.persistentDataPath + "/CreatedLevels/";
        if (!Directory.Exists(levelSavePath))
            Directory.CreateDirectory(levelSavePath);

        //levels.RemoveRange(2, levels.Count - 2);
        levels.Add(new TextAsset(JsonUtility.ToJson(levelRandom)));

        //challengesUnlocked = 1000; //load saved value

        UnlockLevel(PlayerPrefs.GetInt("levelsUnlocked", 1));

        if (Application.platform == RuntimePlatform.WindowsEditor)
            UnlockLevel(9999);
    }

    public Level GetLevel(int levelIndex)
    {
        if (levelIndex < GetLevelCount())
            return JsonUtility.FromJson<Level>(levels[levelIndex].text);

        return null;
    }

    public void UnlockLevel(int newValue)
    {
        if (newValue > levelsUnlocked)
        {
            levelsUnlocked = Math.Clamp(newValue, 1, GetLevelCount());
            PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
        }
    }

    public int GetLevelCount()
    {
        return levels.Count;
    }

    public int GetLevelsUnlocked()
    {
        return levelsUnlocked;
    }

    public bool CheckLevelNameExisting(string levelName)
    {
        string[] files = Directory.GetFiles(levelSavePath);

        foreach (string file in files)
        {
            if (Path.GetFileNameWithoutExtension(file) == levelName)
            {
                return true;
            }
        }

        return false;
    }
}
