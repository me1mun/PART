using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelList : MonoBehaviour
{
    public static LevelList Instance { get; private set; }

    public string saveLevelPath;

    public List<TextAsset> levelJson = new List<TextAsset>();

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

        GameManager.levelCount = levelJson.Count;

        saveLevelPath = Application.persistentDataPath + "/UserLevels/";
    }

    public Level GetLevel(int lvl)
    {
        return JsonUtility.FromJson<Level>(levelJson[lvl].text);
    }
}
