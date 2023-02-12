using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCase : MonoBehaviour
{
    public int levelNum;

    [SerializeField] private Image border;
    [SerializeField] private Color32 borderActiveColor, borderInactiveColor;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private CanvasGroup levelTextCanvas;

    public void Init(int lvlNum)
    {
        levelNum = lvlNum;

        levelText.text = (levelNum + 1).ToString();

        bool isUnlocked = levelNum < GameManager.levelsUnlocked;

        border.color = isUnlocked ? borderActiveColor: borderInactiveColor;
        levelTextCanvas.alpha = isUnlocked ? 1 : 0.5f;

        GetComponent<ButtonController>().SetInteractable(isUnlocked);
    }
}
