using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.Localization;

public class LevelEditor : MonoBehaviour
{

    public TMP_InputField levelNameField;
    //[SerializeField] private string levelName;
    public FieldController field;
    public LevelEditorElementPool elementPool;
    public LevelEditorColorPool colorPool;

    [SerializeField] private TextTransition subtitle;
    [SerializeField] private LocalizedString string_intro, string_existingName, string_noName, string_emptyField, string_noLoop, string_saved;

    private void Awake()
    {
        CreateEmptyField();
    }

    private void OnEnable()
    {
        subtitle.SetText(string_intro);
    }

    public void CreateEmptyField()
    {
        field.CreateField(LevelManager.Instance.levelEmpty, false);
        field.ActivateCellBorders(true, false);
        field.PaintField(colorPool.GetActiveColor());
        levelNameField.text = "";
    }

    public Level ConstructLevel()
    {
        int fieldWidth = field.field.GetLength(0);
        int fieldHeight = field.field.GetLength(1);

        Vector2Int start = new Vector2Int(fieldWidth, fieldHeight);
        Vector2Int end = new Vector2Int(0, 0);

        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x = 0; x < fieldWidth; x++)
            {
                if (field.field[x, y].element.isEmpty == false)
                {
                    start.x = Mathf.Min(start.x, x);
                    end.x = Mathf.Max(end.x, x);

                    start.y = Mathf.Min(start.y, y);
                    end.y = Mathf.Max(end.y, y);
                }
            }
        }

        int width = end.x - start.x + 1;
        int height = end.y - start.y + 1;

        Debug.Log(start + " | " + end);

        string[] elementNameArray = new string[width * height];
        int[] elementFlipArray = new int[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //Debug.Log(x + " , " + y);
                PartController currentCell = field.field[start.x + x, start.y + y];
                int index = y * width + x;

                elementNameArray[index] = currentCell.element.name;

                elementFlipArray[index] = currentCell.flip;
            }
        }

        Level NewLevel = new Level
        {
            levelName = levelNameField.text,
            width = width,
            height = height,
            colorName = colorPool.GetActiveColor().ToString(),
            elements = elementNameArray,
            elementFlip = elementFlipArray,
        };

        return NewLevel;
    }

    public void SaveLevel()
    {
        Level FinalLevel = ConstructLevel();

        if (FinalLevel.width <= 0)
        {
            subtitle.StartTextTransition(string_emptyField);
        }
        else if (field.CheckLoopComplete() == false)
        {
            subtitle.StartTextTransition(string_noLoop);
        }
        else if(FinalLevel.levelName == "")
        {
            subtitle.StartTextTransition(string_noName);
        }
        else if (LevelManager.Instance.CheckLevelNameExisting(FinalLevel.levelName) == true)
        {
            subtitle.StartTextTransition(string_existingName);
        }
        else
        {
            string savePath = LevelManager.Instance.levelSavePath;

            FinalLevel.creationDate = DateTime.Now.ToString();

            string jsonLevel = JsonUtility.ToJson(FinalLevel);

            File.WriteAllText(savePath + FinalLevel.levelName + ".txt", jsonLevel);
            Debug.Log("Level has been saved to: " + savePath);

            //LevelManager.Instance.LoadUserLevels();

            subtitle.StartTextTransition(string_saved);

            CreateEmptyField();
        }
    }
}