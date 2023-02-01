using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LevelEditor : MonoBehaviour
{
    //public Level level;

    public void SaveLevel(Level level)
    {
        string jsonLevel = JsonUtility.ToJson(level);

        File.WriteAllText(Application.dataPath + "/Levels/" + level.nameId + "a.txt", jsonLevel);
        Debug.Log("save completed");
    }
}