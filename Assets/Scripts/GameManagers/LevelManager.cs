using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public enum GameModes { challange, user };

    public GameModes gameMode = GameModes.challange;
    public int level = 0;
    public int challangesUnlocked = 1;

    public string userLevelPath;

    private Dictionary<GameModes, List<TextAsset>> levels = new Dictionary<GameModes, List<TextAsset>>();

    [SerializeField] private List<TextAsset> levelChallangeList = new List<TextAsset>();
    private List<TextAsset> levelCustomList = new List<TextAsset>();

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

        challangesUnlocked = 1000; //load saved value
        
        levels.Add(GameModes.challange, levelChallangeList);
        levels.Add(GameModes.user, levelCustomList);

        levels[GameModes.challange].Add(new TextAsset(JsonUtility.ToJson(levelRandom)));
        LoadUserLevels();
    }

    public void LoadUserLevels()
    {
        levels[GameModes.user].Clear();
        string[] userLevelNames = Directory.GetFiles(userLevelPath, "*.txt");

        foreach (string fileName in userLevelNames)
        {
            byte[] fileData = File.ReadAllBytes(fileName);
            string fileText = System.Text.Encoding.UTF8.GetString(fileData);

            levels[GameModes.user].Add(new TextAsset(fileText));
        }
    }


    public Level GetLevel(GameModes mode, int levelIndex)
    {
        //Debug.Log("Init " + levelIndex + ": " + levels[GameModes.user][0].text);
        return JsonUtility.FromJson<Level>(levels[mode][levelIndex].text);
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

    public void UnlockChallange()
    {

        if (level >= challangesUnlocked - 1 && challangesUnlocked < GetLevelCount(GameModes.challange))
        {
            challangesUnlocked += 1;
        }
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
