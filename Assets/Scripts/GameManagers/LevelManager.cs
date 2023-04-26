using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public enum GameModes { challenge, random, user, premium};

    public int challengesUnlocked = 1;

    public string userLevelPath;

    private Dictionary<GameModes, List<Level>> levels = new Dictionary<GameModes, List<Level>>();

    [SerializeField] private List<TextAsset> challengeLevelTemp = new List<TextAsset>();
    [SerializeField] private List<TextAsset> premiumLevelTemp = new List<TextAsset>();

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

        userLevelPath = Application.persistentDataPath + "/UserLevels/";
        if (!Directory.Exists(userLevelPath))
            Directory.CreateDirectory(userLevelPath);

        //challengesUnlocked = 1000; //load saved value

        foreach (GameModes gm in Enum.GetValues(typeof(GameModes)))
        {
            levels.Add(gm, new List<Level>());
        }
        
        //Debug.Log("Levels: " + GetLevelCount(GameModes.challenge));
        LoadChallengeLevels();
        LoadRandomLevels();
        LoadUserLevels();
        LoadPremiumLevels();
    }

    public void LoadChallengeLevels()
    {
        levels[GameModes.challenge].Clear();

        foreach (TextAsset txt in challengeLevelTemp)
        {
            Level newLevel = JsonUtility.FromJson<Level>(txt.text);
            levels[GameModes.challenge].Add(newLevel);
        }

        levels[GameModes.challenge].Add(levelRandom);

        //challengeLevelTemp.Clear();
    }

    public void LoadUserLevels()
    {
        levels[GameModes.user].Clear();
        string[] userLevelName = Directory.GetFiles(userLevelPath, "*.txt");

        foreach (string fileName in userLevelName)
        {
            byte[] fileData = File.ReadAllBytes(fileName);
            string fileText = System.Text.Encoding.UTF8.GetString(fileData);

            levels[GameModes.user].Add(JsonUtility.FromJson<Level>(fileText));
            
        }

        levels[GameModes.user] = levels[GameModes.user].OrderByDescending(l => DateTime.Parse(l.creationDate)).ToList();
    }

    public void LoadPremiumLevels()
    {
        levels[GameModes.premium].Clear();

        foreach (TextAsset txt in premiumLevelTemp)
        {
            Level newLevel = JsonUtility.FromJson<Level>(txt.text);
            levels[GameModes.premium].Add(newLevel);
            
        }

        //premiumLevelTemp.Clear();
    }

    public void LoadRandomLevels()
    {
        levels[GameModes.random].Clear();

        levels[GameModes.random].Add(levelRandom);

        //premiumLevelTemp.Clear();
    }

    public Level GetLevel(GameModes gm, int levelIndex)
    {
        if (levelIndex < GetLevelCount(gm))
            return levels[gm][levelIndex];

        return null;
    }

    public int GetLevelCount(GameModes mode)
    {
        return levels[mode].Count;
    }

    public bool CheckUserLevelExisting(string userLevelName)
    {
        foreach (Level lvl in levels[GameModes.user])
        {
            if (lvl.levelName.ToLower() == userLevelName.ToLower())
                return true;
        }

        return false;
    }

    public void DeleteUserLevel(string fileName)
    {
        string filePath = Path.Combine(userLevelPath, fileName + ".txt");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("File deleted: " + filePath);
        }
        else
        {
            Debug.Log("File not found: " + filePath);
        }

        LoadUserLevels();
    }
}

[System.Serializable]
public class GameModeInfo
{
    public LevelManager.GameModes gameMode;

    public bool isUnlockable;
    public int levelsComplete;
    public bool loopedSequence;

    public bool isPremium;
    public bool isInfinite;
    public bool displayTitle;
    public bool displayLevelName;

    public string GetSaveKey()
    {
        return "levelsComplete_" + gameMode.ToString();
    }
}
