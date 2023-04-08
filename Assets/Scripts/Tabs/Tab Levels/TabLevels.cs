using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabLevels : MonoBehaviour
{
    [SerializeField] private GameController game;
    [SerializeField] private InfiniteScroll scroll;

    public void StartLevel(int levelIndex)
    {
        GameManager.SetLevel(levelIndex);
        game.StartLevel();
        //Debug.Log("New level: " + GameManager.level);
        scroll.InitCases();
    }
}
