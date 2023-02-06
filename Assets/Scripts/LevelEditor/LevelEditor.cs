using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class LevelEditor : MonoBehaviour
{
    //public Level level;

    public static LevelEditor Instance { get; private set; }

    public TMP_InputField levelNameField;
    public LevelEditorElementPool elementPool;
    public LevelEditorField field;
    public LevelEditorColorPool colorPool;

    [SerializeField] private TextMeshProUGUI subTitle;

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
    }

    public Level ConstructLevel()
    {
        int fieldWidth = field.cellsList.GetLength(0);
        int fieldHeight = field.cellsList.GetLength(1);

        Vector2Int start = new Vector2Int(fieldWidth, fieldHeight);
        Vector2Int end = new Vector2Int(0, 0);

        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x = 0; x < fieldWidth; x++)
            {
                if (field.cellsList[x, y].element != null)
                {
                    start.x = Mathf.Min(start.x, x);
                    end.x = Mathf.Max(start.x, x);

                    start.y = Mathf.Min(start.y, y);
                    end.y = Mathf.Max(start.y, y);
                }
            }
        }

        int width = end.x - start.x + 1;
        int height = end.y - start.y + 1;

        Debug.Log(start + " | " + end);

        string[] elementNames = new string[width * height];
        int[] elementFlip = new int[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //Debug.Log(x + " , " + y);
                LevelEditorCell currentCell = field.cellsList[start.x + x, start.y + y];
                int index = y * width + x;

                if (currentCell.element != null)
                {
                    elementNames[index] = currentCell.element.name;
                }
                else
                {
                    elementNames[index] = "null";
                }
                
                elementFlip[index] = currentCell.elementFlip;
            }
        }

        Level NewLevel = new Level
        {
            nameId = levelNameField.text,
            width = width,
            height = height,
            colorName = colorPool.GetActiveColor().ToString(),
            elements = elementNames,
            elementFlip = elementFlip,
        };

        return NewLevel;
    }

    public void SaveLevel()
    {
        Level FinalLevel = ConstructLevel();

        if (FinalLevel.nameId == "")
        {
            subTitle.text = "Enter the level name";
        }
        else if (FinalLevel.width <= 0)
        {
            subTitle.text = "No level here";
        }
        else
        {
            string savePath = Application.persistentDataPath + "/Levels/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string jsonLevel = JsonUtility.ToJson(FinalLevel);

            File.WriteAllText(savePath + levelNameField.text + ".txt", jsonLevel);
            Debug.Log("saved to: " + savePath);
        }
    }
}