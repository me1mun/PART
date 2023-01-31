using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldController : MonoBehaviour
{
    private Level currentLevel;
    [SerializeField] GameObject elementPrefab;

    [SerializeField] GameObject field;
    private GameObject[,] fieldList = new GameObject[0, 0];
    private int fieldWidth, fieldHeight;
    //private Element[,] elementsList;

    void Start()
    {
        
        FieldCreate();
    }

    private void FieldClear()
    {
        foreach(GameObject el in fieldList)
        {
            Destroy(el);
        }
    }

    public void FieldCreate(bool shuffle = false)
    {
        FieldClear();
        
        currentLevel = LevelData.levels[0];//LevelData.Levels[GameManager.level];
        fieldWidth = currentLevel.width;
        fieldHeight = currentLevel.parts.Length / fieldWidth;

        GetComponent<GridLayoutGroup>().constraintCount = fieldWidth;
        fieldList = new GameObject[fieldWidth, fieldHeight];

        for (int i = 0; i < fieldWidth; i++)
        {
            for (int j = 0; j < fieldHeight; j++)
            {
                int elementIndex = j + i * fieldHeight;
                //Debug.Log("Index: " + elementIndex);
                fieldList[i, j] = Instantiate(elementPrefab, field.transform);
                ElementController newElement = fieldList[i, j].GetComponent<ElementController>();

                newElement.Init(currentLevel.parts[elementIndex], currentLevel);
                
            }
        }

        if (shuffle)
        {
            fieldShuffle(true);
        }
    }

    public void fieldShuffle(bool flashShuffle)
    {
        foreach (GameObject el in fieldList)
        {
            el.GetComponent<ElementController>().ElementTurn(Random.Range(0, 4), flashShuffle);
        }
    }
}
