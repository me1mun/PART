using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCaseMenu : MonoBehaviour
{
    public int levelNum = 0;
    public bool isLocked;
    [SerializeField] Button button;
    [SerializeField] private Color32 colorPremium;
    [SerializeField] private GameObject objectLocked, objectUnlocked;
    [SerializeField] private Image caseStroke;
    [SerializeField] private CanvasGroup caseStrokeGroup;
    [SerializeField] private TextMeshProUGUI levelNumText;

    private void Awake()
    {
        //Init(1);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init(int num)
    {
        levelNum = num;
        levelNumText.text = "#" + (num + 1).ToString();

        SetDisplay(levelNum > GameManager.levelLast);
    }

    private void SetDisplay(bool disable)
    {
        isLocked = disable;

        button.interactable = !isLocked;

        objectUnlocked.SetActive(!isLocked);
        objectLocked.SetActive(isLocked);

        caseStrokeGroup.alpha = isLocked ? 0.3f : 1;
    }
}
