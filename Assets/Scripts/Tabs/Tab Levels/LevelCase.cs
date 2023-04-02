using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCase : MonoBehaviour
{
    
    //public GameController.LevelType listType = GameController.LevelType.challange;
    public int levelIndex;

    private InfiniteScrollCase scrollCase;
    [SerializeField] private GameObject outline;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private CanvasGroup levelTextCanvas;

    private void Awake()
    {
        scrollCase = GetComponent<InfiniteScrollCase>();
        scrollCase.OnInit.AddListener(Init);
    }

    public void Init()
    {
        levelIndex = scrollCase.GetIndex();
        Level level = LevelList.Instance.GetLevel(levelIndex);
        //field.FieldCreate(level);

        levelText.text = (levelIndex + 1).ToString();

        bool isUnlocked = levelIndex < GameManager.levelsUnlocked;

        float disableAlpha = 0.33f;
        levelTextCanvas.alpha = isUnlocked ? 1 : disableAlpha;
        GetComponent<ButtonController>().SetInteractable(isUnlocked);

        outline.gameObject.SetActive(GameManager.level == levelIndex);
    }

    public int GetLevelIndex()
    {
        return levelIndex;
    }
}
