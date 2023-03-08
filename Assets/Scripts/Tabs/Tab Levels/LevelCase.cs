using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCase : MonoBehaviour
{
    public int levelIndex;

    [SerializeField] private Image border;
    [SerializeField] private Color32 borderActiveColor, borderInactiveColor;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private CanvasGroup levelTextCanvas;

    public void Init(int lvlNum)
    {
        levelIndex = lvlNum;

        levelText.text = (levelIndex + 1).ToString();

        bool isUnlocked = levelIndex < GameManager.levelsUnlocked;

        border.color = isUnlocked ? borderActiveColor: borderInactiveColor;
        levelTextCanvas.alpha = isUnlocked ? 1 : 0.5f;

        GetComponent<ButtonController>().SetInteractable(isUnlocked);
    }

    public int GetLevelIndex()
    {
        return levelIndex;
    }
}
