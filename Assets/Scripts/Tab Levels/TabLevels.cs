using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;

public class TabLevels : MonoBehaviour
{
    private LevelManager.GameModes currentListMode;
    [SerializeField] private GameController game;
    [SerializeField] private InfiniteScroll challengeScroll, userScroll;
    [SerializeField] private TextTransition titleTransition;
    private ListModeButton[] listModeButtonList;

    private void Awake()
    {
        listModeButtonList = GetComponentsInChildren<ListModeButton>(true);

    }

    private void OnEnable()
    {
        SetListMode(currentListMode);
    }

    public void ReloadListCases()
    {
        challengeScroll.CreateCases(LevelManager.Instance.GetLevelCount(LevelManager.GameModes.challenge));
        userScroll.CreateCases(LevelManager.Instance.GetLevelCount(LevelManager.GameModes.user));
    }

    public void StartLevelFlash(int levelIndex)
    {
        LevelManager.Instance.SetLevel(currentListMode, levelIndex);
        game.StartLevel(false);

        //scroll.InitCases();
    }

    public void SetListMode(LevelManager.GameModes newListMode)
    {
        currentListMode = newListMode;

        foreach(ListModeButton btn in listModeButtonList)
        {
            btn.Activate(btn.GetListMode() == currentListMode);
        }

        if(newListMode == LevelManager.GameModes.challenge)
        {
            userScroll.gameObject.SetActive(false);
            challengeScroll.gameObject.SetActive(true);
            challengeScroll.CreateCases(LevelManager.Instance.GetLevelCount(newListMode));
        }
        else if (newListMode == LevelManager.GameModes.user)
        {
            challengeScroll.gameObject.SetActive(false);
            userScroll.gameObject.SetActive(true);
            userScroll.CreateCases(LevelManager.Instance.GetLevelCount(newListMode));
        }
    }
}
