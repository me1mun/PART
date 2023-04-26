using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollChallengeLevelCase : MonoBehaviour
{
    
    //public GameController.LevelType listType = GameController.LevelType.challange;
    public int levelIndex;

    private TabLevels tabLevels;
    private InfiniteScrollCase scrollCase;
    private Level level;

    [SerializeField] private GameObject challengeContent, randomContent;
    [SerializeField] private GameObject fill, outlineActive, outlineLock;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private CanvasGroup contentCanvasGroup;

    private void Awake()
    {
        scrollCase = GetComponent<InfiniteScrollCase>();
        scrollCase.OnInit.AddListener(Init);
    }

    public void Init()
    {
        levelIndex = scrollCase.GetIndex();
        level = LevelManager.Instance.GetLevel(LevelManager.GameModes.challenge, levelIndex);
        //field.FieldCreate(level);

        levelText.text = (levelIndex + 1).ToString();

        bool isUnlocked = levelIndex <= GameController.Instance.GetLevelsComplete(LevelManager.GameModes.challenge) || level.isRandom;

        GetComponent<ButtonController>().SetInteractable(isUnlocked);
        fill.SetActive(isUnlocked);
        outlineLock.SetActive(!isUnlocked);

        bool isRandom = level.isRandom;
        contentCanvasGroup.alpha = isUnlocked ? 1 : 0.33f;
        challengeContent.SetActive(!isRandom);
        randomContent.SetActive(isRandom);

        bool isActive = false;

        if (GameController.Instance.gameMode == LevelManager.GameModes.challenge)
            isActive = GameController.Instance.levelIndex == levelIndex;

        if (GameController.Instance.gameMode == LevelManager.GameModes.random && isRandom)
            isActive = true;

        outlineActive.gameObject.SetActive(isActive);
    }

    public void Interact()
    {
        tabLevels = GetComponentInParent<TabLevels>();

        if(level.isRandom)
            tabLevels.StartLevelFlash(LevelManager.GameModes.random, 0);
        else
            tabLevels.StartLevelFlash(LevelManager.GameModes.challenge, levelIndex);
    }
}
