using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FieldController : MonoBehaviour
{
    private Level currentLevel;
    [SerializeField] Transform fieldContainer;
    [SerializeField] GameObject casePrefab;

    public PartController[,] field = new PartController[0, 0];
    private Vector2Int fieldSize;

    void Start()
    {
        //currentLevel = JsonUtility.FromJson<Level>(LevelList.Instance.levelJson[GameManager.level].text);

        //FieldCreate(currentLevel);
    }

    public void FieldCreate(Level level)
    {
        FieldClear();
        
        if(level != null)
        {
            fieldSize.x = level.width;
            fieldSize.y = level.height;
        }
        else
        {
            fieldSize.x = 8;
            fieldSize.y = 10;
        }
        

        GetComponent<GridLayoutGroup>().constraintCount = fieldSize.x;
        field = new PartController[fieldSize.x, fieldSize.y];

        for (int y = 0; y < fieldSize.y; y++)
        {
            for (int x = 0; x < fieldSize.x; x++)
            {
                int elementIndex = y * fieldSize.x + x;
                //Debug.Log("Index: " + elementIndex);
                field[x, y] = Instantiate(casePrefab, fieldContainer).GetComponent<PartController>();


                Element newPart = LevelDatabase.Instance.emptyElemet;
                int newPartFlip = 0;
                Color32 levelColor = LevelDatabase.Instance.defaultColor;

                if (level != null)
                {
                    newPart = LevelDatabase.Instance.GetElement(level.elements[elementIndex]);
                    levelColor = LevelDatabase.Instance.GetColor(Enum.Parse<LevelDatabase.Colors>(level.colorName));

                    if (newPart.isFixed)
                        newPartFlip = level.elementFlip[elementIndex];
                    else
                        newPartFlip = UnityEngine.Random.Range(0, 4);
                }

                
                field[x, y].Init(newPart, newPartFlip);
                field[x, y].PaintSelf(levelColor);
            }
        }

        //Destroy(casePrefab);
    }

    private void FieldClear()
    {
        foreach (PartController el in field)
        {
            Destroy(el.gameObject);
        }
    }

    public void fieldShuffle(bool flashShuffle)
    {
        foreach (PartController el in field)
        {
            if(!el.isFixed)
            {
                el.FlipElement(UnityEngine.Random.Range(0, 4), flashShuffle);
            }
        }
    }

    public void PaintField(LevelDatabase.Colors colorName)
    {
        Color32 newColor = LevelDatabase.Instance.GetColor(colorName);

        foreach (PartController ec in field)
        {
            ec.PaintSelf(newColor);
        }
    }
}
