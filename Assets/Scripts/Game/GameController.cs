using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public enum LevelType { challange, custom };

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private FieldController field;
    [SerializeField] private TextMeshProUGUI levelCounter;

    private Level level;
    private LevelType levelType;

    //private float initProgress;
    private Coroutine levelInitCoroutine;

    void Start()
    {
        StartLevel(LevelList.Instance.GetLevel(GameManager.level), LevelType.challange);
    }

    public void ChangeLevel(int count)
    {
        GameManager.ChangeLevel(count);
    }

    public void StartLevel(Level newLevel, LevelType newLevelType)
    {
        level = newLevel;
        levelType = newLevelType;

        field.FieldCreate(level);
    }
}
