using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelList : MonoBehaviour
{
    public static LevelList Instance { get; private set; }

    public string saveLevelPath;

    public List<TextAsset> levelJson = new List<TextAsset>();
    [SerializeField] private TextAsset randomLevel;

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

        levelJson.Add(randomLevel);
    }

    public Level GetLevel(int lvl)
    {
        return JsonUtility.FromJson<Level>(levelJson[lvl].text);
    }

    public int GetLevelCount()
    {
        return levelJson.Count;
    }
}
