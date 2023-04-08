using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int level = 0;
    public int levelsUnlocked = 1;
    public int levelCount;

    public string saveLevelPath;

    [SerializeField] private TextAsset[] levelsJson;
    private List<Level> levels = new List<Level>();


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

        saveLevelPath = Application.persistentDataPath + "/CreatedLevels/";

        foreach (TextAsset tl in levelsJson)
            levels.Add(JsonUtility.FromJson<Level>(tl.text));
        levels.Add(LevelDatabase.randomLevel);

        levelCount = levels.Count;
    }

    public Level GetLevel(int levelInex)
    {
        return levels[levelInex];
    }

    public int GetLevelCount()
    {
        return levels.Count;
    }

    public void SetLevel(int newLevel)
    {
        if (newLevel >= 0 && newLevel < levelsUnlocked)
        {
            level = Mathf.Clamp(newLevel, 0, levelCount);
        }
    }

    public void UnlockLevel()
    {

        if (level >= levelsUnlocked - 1 && levelsUnlocked < levelCount)
        {
            levelsUnlocked += 1;
        }
    }
}
