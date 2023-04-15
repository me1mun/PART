using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public enum GameModes { challenge, user };

    private string saveKeyChallengesUnlocked = "challengesUnlocked";
    public GameModes gameMode = GameModes.challenge;
    public int level = 0;
    public int challengesUnlocked = 1;

    public string userLevelPath;

    private Dictionary<GameModes, List<Level>> levels = new Dictionary<GameModes, List<Level>>();

    [SerializeField] private List<TextAsset> challengeLevelTemp = new List<TextAsset>();
    private List<Level> challengeLevelList = new List<Level>();
    private List<Level> userLevelList = new List<Level>();

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
        
        levels.Add(GameModes.challenge, challengeLevelList);
        levels.Add(GameModes.user, userLevelList);

        
        //Debug.Log("Levels: " + GetLevelCount(GameModes.challenge));
        LoadChallengeLevels();
        LoadUserLevels();
        LoadData();
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

    public bool CheckUserLevelExisting(string userLevelName)
    {
        foreach(Level lvl in levels[GameModes.user])
        {
            if (lvl.levelName.ToLower() == userLevelName.ToLower())
                return true;
        }

        return false;
    }

    private void LoadData()
    {

        UnlockChallenge(PlayerPrefs.GetInt(saveKeyChallengesUnlocked, 1));

        level = challengesUnlocked - 1;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(saveKeyChallengesUnlocked, challengesUnlocked);
    }

    public void SetLevel(GameModes newGameMode, int newLevel)
    {
        gameMode = newGameMode;
        level = Mathf.Clamp(newLevel, 0, GetLevelCount(gameMode) - 1);
    }

    public Level GetLevel(GameModes mode, int levelIndex)
    {
        return levels[mode][levelIndex];
    }

    public int GetLevelCount(GameModes mode)
    {
        return levels[mode].Count;
    }

    public void UnlockChallenge(int newValue)
    {
        newValue = Mathf.Clamp(newValue, 1, GetLevelCount(GameModes.challenge));
        challengesUnlocked = newValue;

        SaveData();
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
