using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelList : MonoBehaviour
{
    public static LevelList Instance { get; private set; }

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
    }

    public Level GetLevel(int levelInex)
    {
        return levels[levelInex];
    }

    public int GetLevelCount()
    {
        return levels.Count;
    }
}
