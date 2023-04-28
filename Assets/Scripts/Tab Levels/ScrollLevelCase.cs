using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollLevelCase : MonoBehaviour
{
    
    //public GameController.LevelType listType = GameController.LevelType.challange;
    public int levelIndex;

    private TabLevels tabLevels;
    private InfiniteScrollCase scrollCase;
    private Level level;

    [SerializeField] private GameObject levelContent, randomContent;
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
        level = LevelManager.Instance.GetLevel(levelIndex);

        levelText.text = (levelIndex + 1).ToString();

        bool isUnlocked = levelIndex < LevelManager.Instance.GetLevelsUnlocked();

        GetComponent<ButtonController>().SetInteractable(isUnlocked);
        fill.SetActive(isUnlocked);
        outlineLock.SetActive(!isUnlocked);

        bool isRandom = level.isRandom;
        contentCanvasGroup.alpha = isUnlocked ? 1 : 0.33f;
        levelContent.SetActive(!isRandom);
        randomContent.SetActive(isRandom);

        bool isCurrent = false;

        if (levelIndex == GameController.Instance.levelIndex)
            isCurrent = true;

        outlineActive.gameObject.SetActive(isCurrent);
    }

    public void Interact()
    {
        tabLevels = GetComponentInParent<TabLevels>();

        if(level.isRandom)
            tabLevels.StartLevelFlash(levelIndex);
        else
            tabLevels.StartLevelFlash(levelIndex);
    }
}
