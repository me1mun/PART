using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCaseRow : MonoBehaviour
{
    [SerializeField] private int rowNum;
    private int casesInRow = 5;

    [SerializeField] LevelCase[] levelCase = new LevelCase[5];

    private void Start()
    {
        //Init(0);
    }

    public void Init(int rn)
    {
        rowNum = rn;

        for(int i = 0; i < casesInRow; i++)
        {
            int levelIndex = rowNum * 5 + i;

            levelCase[i].gameObject.SetActive(levelIndex < GameManager.levelCount);
            levelCase[i].Init(levelIndex);
        }
    }

    public int GetRowNum()
    {
        return rowNum;
    }
}
