using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameField : MonoBehaviour
{
    private Level currentLevel;
    [SerializeField] GameObject casePrefab;

    private GameElement[,] field = new GameElement[0, 0];
    private int fieldWidth, fieldHeight;
    //private Element[,] elementsList;

    void Start()
    {
        
        FieldCreate();
    }

    private void FieldClear()
    {
        foreach(GameElement el in field)
        {
            Destroy(el.gameObject);
        }
    }

    public void FieldCreate()
    {
        FieldClear();

        currentLevel = JsonUtility.FromJson<Level>(LevelList.Instance.levelJson[GameManager.level].text);
        Color32 levelColor = LevelDatabase.Instance.GetColor(Enum.Parse<LevelDatabase.Colors>(currentLevel.colorName));
        fieldWidth = currentLevel.width;
        fieldHeight = currentLevel.height;

        GetComponent<GridLayoutGroup>().constraintCount = fieldWidth;
        field = new GameElement[fieldWidth, fieldHeight];

        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x = 0; x < fieldWidth; x++)
            {
                int elementIndex = x + y * fieldHeight;
                //Debug.Log("Index: " + elementIndex);
                field[x, y] = Instantiate(casePrefab, casePrefab.transform.parent).GetComponent<GameElement>();


                Element newElement = LevelDatabase.Instance.GetElement(currentLevel.elements[elementIndex]);

                int newElementFlip = UnityEngine.Random.Range(0, 4);
                if (newElement != null && newElement.isFixed)
                {
                    newElementFlip = currentLevel.elementFlip[elementIndex];
                }
                
                field[x, y].Init(newElement, levelColor, newElementFlip);
            }
        }

        Destroy(casePrefab);
    }

    public void fieldShuffle(bool flashShuffle)
    {
        foreach (GameElement el in field)
        {
            if(!el.isFixed)
            {
                el.ElementTurn(UnityEngine.Random.Range(0, 4), flashShuffle);
            }
        }
    }
}
