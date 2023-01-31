using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LevelEditor : MonoBehaviour
{
    public NewLevel level;

    public void SaveLevel()
    {
        string jsonLevel = JsonUtility.ToJson(level);

        File.WriteAllText(Application.dataPath + "/Levels/" + level.nameId + "a.txt", jsonLevel);
        Debug.Log("save completed");
    }
}

[Serializable]
public class NewLevel
{
    public string nameId;
    public int width;

    public enum Color { blue, red, green, yellow, purple };
    public Color color;

    public Part[] parts;
}

[Serializable]
public class NewPart
{
    public Element element;

    public bool isFixed = false;
    public int fixTurns;
}