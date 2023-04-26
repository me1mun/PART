using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollPremiumChallengeCase : MonoBehaviour 
{
    public int levelIndex;

    private TabLevels tabLevels;
    private InfiniteScrollCase scrollCase;
    private Level level;

    bool isUnlocked = false;
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

        isUnlocked = levelIndex <= GameController.Instance.GetLevelsComplete(LevelManager.GameModes.premium);
        if (PremiumManager.isPremium == false)
            isUnlocked = false;

        GetComponent<ButtonController>().SetInteractable(isUnlocked);
        fill.SetActive(isUnlocked);
        outlineLock.SetActive(!isUnlocked);

        bool isRandom = level.isRandom;
        contentCanvasGroup.alpha = isUnlocked ? 1 : 0.33f;


        if (GameController.Instance.gameMode == LevelManager.GameModes.premium)
            outlineActive.gameObject.SetActive(GameController.Instance.levelIndex == levelIndex);
    }

    public void Interact()
    {
        tabLevels = GetComponentInParent<TabLevels>();

        tabLevels.StartLevelFlash(LevelManager.GameModes.premium, levelIndex);
    }
}
