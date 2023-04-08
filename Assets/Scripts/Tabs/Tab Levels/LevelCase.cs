using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCase : MonoBehaviour
{
    
    //public GameController.LevelType listType = GameController.LevelType.challange;
    public int levelIndex;

    private TabLevels tabLevels;
    private InfiniteScrollCase scrollCase;

    [SerializeField] private GameObject containerInfo, containerInfinite;

    [SerializeField] private GameObject outline;
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
        Level level = LevelManager.Instance.GetLevel(levelIndex);
        //field.FieldCreate(level);

        levelText.text = (levelIndex + 1).ToString();

        bool isUnlocked = levelIndex < LevelManager.Instance.levelsUnlocked;

        float disableAlpha = 0.33f;
        contentCanvasGroup.alpha = isUnlocked ? 1 : disableAlpha;
        GetComponent<ButtonController>().SetInteractable(isUnlocked);

        outline.gameObject.SetActive(LevelManager.Instance.level == levelIndex);

        bool isRandom = level.levelType == LevelDatabase.LevelTypes.random;
        containerInfo.gameObject.SetActive(!isRandom);
        containerInfinite.gameObject.SetActive(isRandom);

    }

    public void Interact()
    {
        if(LevelManager.Instance.level != levelIndex)
        {
            tabLevels = GetComponentInParent<TabLevels>();
            tabLevels.StartLevelFlash(levelIndex);


        }

    }
}
