using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;

public class TabLevels : MonoBehaviour
{
    private LevelManager.GameModes currentListMode;
    [SerializeField] private GameController game;
    [SerializeField] private InfiniteScroll challangeScroll, userScroll;
    [SerializeField] private TextTransition titleTransition;
    [SerializeField] private LocalizedString challangeTitle, userTitle;
    private ListModeButton[] listModeButtonList;

    private void Awake()
    {
        listModeButtonList = GetComponentsInChildren<ListModeButton>(true);

    }

    private void OnEnable()
    {
        SetListMode(currentListMode);
        //StartCoroutine(OnEndOfFrame());
    }

    IEnumerator OnEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        SetListMode(currentListMode);
    }

    public void ReloadListCases()
    {
        challangeScroll.CreateCases(LevelManager.Instance.GetLevelCount(LevelManager.GameModes.challange));
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

        if(newListMode == LevelManager.GameModes.challange)
        {
            titleTransition.StartTextTransition(challangeTitle, 0);
            userScroll.gameObject.SetActive(false);
            challangeScroll.gameObject.SetActive(true);
            challangeScroll.CreateCases(LevelManager.Instance.GetLevelCount(newListMode));
        }
        else if (newListMode == LevelManager.GameModes.user)
        {
            titleTransition.StartTextTransition(userTitle, 0);
            challangeScroll.gameObject.SetActive(false);
            userScroll.gameObject.SetActive(true);
            userScroll.CreateCases(LevelManager.Instance.GetLevelCount(newListMode));
        }
    }
}
