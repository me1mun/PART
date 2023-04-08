using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabLevels : MonoBehaviour
{
    [SerializeField] private GameController game;
    [SerializeField] private InfiniteScroll scroll;

    public void StartLevelFlash(int levelIndex)
    {
        LevelManager.Instance.SetLevel(levelIndex);
        game.StartLevel(false);
        //Debug.Log("New level: " + GameManager.level);
        //scroll.InitCases();
    }
}
