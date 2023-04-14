using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public enum GameModes { challange, user };

    private string saveKeyChallangesUnlocked = "challangesUnlocked";
    public GameModes gameMode = GameModes.challange;
    public int level = 0;
    public int challangesUnlocked = 1;

    public string userLevelPath;

    private Dictionary<GameModes, List<Level>> levels = new Dictionary<GameModes, List<Level>>();

    [SerializeField] private List<TextAsset> challangeLevelTemp = new List<TextAsset>();
    private List<Level> challangeLevelList = new List<Level>();
    private List<Level> userLevelList = new List<Level>();
    private string[] userLevelName;

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

        //challangesUnlocked = 1000; //load saved value
        
        levels.Add(GameModes.challange, challangeLevelList);
        levels.Add(GameModes.user, userLevelList);

        LoadChallangeLevels();
        LoadUserLevels();

        LoadData();
    }

    public void LoadChallangeLevels()
    {
        levels[GameModes.challange].Clear();

        foreach (TextAsset txt in challangeLevelTemp)
        {
            Level newLevel = JsonUtility.FromJson<Level>(txt.text);
            levels[GameModes.challange].Add(newLevel);
        }

        levels[GameModes.challange].Add(levelRandom);
    }

    public void LoadUserLevels()
    {
        levels[GameModes.user].Clear();
        userLevelName = Directory.GetFiles(userLevelPath, "*.txt");

        foreach (string fileName in userLevelName)
        {
            byte[] fileData = File.ReadAllBytes(fileName);
            string fileText = System.Text.Encoding.UTF8.GetString(fileData);

            levels[GameModes.user].Add(JsonUtility.FromJson<Level>(fileText));
        }
    }

    private void LoadData()
    {
        UnlockChallange(PlayerPrefs.GetInt(saveKeyChallangesUnlocked, 1));
        level = challangesUnlocked - 1;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(saveKeyChallangesUnlocked, challangesUnlocked);
    }

    public Level GetLevel(GameModes mode, int levelIndex)
    {
        //Debug.Log("Init " + levelIndex + ": " + levels[GameModes.user][0].text);
        return levels[mode][levelIndex];
    }

    public int GetLevelCount(GameModes mode)
    {
        return levels[mode].Count;
    }

    public void SetLevel(GameModes newGameMode, int newLevel)
    {
        gameMode = newGameMode;
        level = Mathf.Clamp(newLevel, 0, GetLevelCount(gameMode) - 1);
    }

    public void UnlockChallange(int newValue)
    {
        newValue = newValue % GetLevelCount(GameModes.challange);

        challangesUnlocked = newValue;
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
