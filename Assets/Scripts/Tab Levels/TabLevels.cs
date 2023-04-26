using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;

public class TabLevels : MonoBehaviour
{
    private LevelManager.GameModes currentListMode;
    [SerializeField] private List<LevelsTab> levelsTablList;
    [SerializeField] private GameController game;
    [SerializeField] private Menu menu;
    [SerializeField] private InfiniteScroll challengeScroll, userScroll;
    [SerializeField] private TextTransition titleTransition;

    private void Awake()
    {

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

    public void StartLevelFlash(LevelManager.GameModes gm, int levelIndex)
    {
        game.SetLevel(gm, levelIndex);
        game.StartLevel(false);

        //scroll.InitCases();
    }

    public void OpenPremiumTab()
    {
        menu.OpenTab(TabManager.TabEnum.premium);
    }

    public void SetListMode(LevelManager.GameModes newListMode)
    {
        currentListMode = newListMode;

        foreach(LevelsTab lvltab in levelsTablList)
        {
            bool modeIsMatch = lvltab.gameMode == currentListMode;

            lvltab.button.Activate(modeIsMatch);

            lvltab.scroll.gameObject.SetActive(modeIsMatch);
            lvltab.scroll.CreateCases(LevelManager.Instance.GetLevelCount(newListMode));
        }
    }
}

[System.Serializable]
public class LevelsTab
{
    public LevelManager.GameModes gameMode;

    public LevelsTabButton button;
    public InfiniteScroll scroll;
}