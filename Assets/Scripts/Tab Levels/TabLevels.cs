using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;

public class TabLevels : MonoBehaviour
{
    [SerializeField] private GameController game;
    [SerializeField] private Menu menu;
    [SerializeField] private InfiniteScroll levelScroll;
    [SerializeField] private TextTransition titleTransition;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        ReloadListCases();
    }

    public void ReloadListCases()
    {
        levelScroll.CreateCases(LevelManager.Instance.GetLevelCount());
    }

    public void StartLevelFlash(int levelIndex)
    {
        game.SetLevel(levelIndex);
        game.StartLevel(false);

        //scroll.InitCases();
    }

    public void OpenPremiumTab()
    {
        menu.OpenTab(TabManager.TabEnum.premium);
    }
}