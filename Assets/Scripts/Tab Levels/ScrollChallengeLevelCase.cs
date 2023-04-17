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
        Level level = LevelManager.Instance.GetLevel(LevelManager.GameModes.challenge, levelIndex);
        //field.FieldCreate(level);

        levelText.text = (levelIndex + 1).ToString();

        bool isUnlocked = levelIndex <= GameController.Instance.GetLevelsComplete(LevelManager.GameModes.challenge);

        GetComponent<ButtonController>().SetInteractable(isUnlocked);

        float disableAlpha = 0.33f;
        contentCanvasGroup.alpha = isUnlocked ? 1 : disableAlpha;
        fill.SetActive(isUnlocked);
        outlineLock.SetActive(!isUnlocked);

        if(GameController.Instance.gameMode == LevelManager.GameModes.challenge)
            outlineActive.gameObject.SetActive(GameController.Instance.levelIndex == levelIndex);
    }

    public void Interact()
    {
        tabLevels = GetComponentInParent<TabLevels>();
        tabLevels.StartLevelFlash(LevelManager.GameModes.challenge, levelIndex);
    }
}
