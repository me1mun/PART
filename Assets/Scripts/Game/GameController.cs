using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private FieldController field;
    [SerializeField] private TextMeshProUGUI levelCounter;

    private Level currentLevel;

    //private float initProgress;
    private Coroutine levelinitCoroutine;

    void Start()
    {
        field.FieldCreate(LevelList.Instance.GetLevel());
    }

    public void ChangeLevel(int count)
    {
        GameManager.ChangeLevel(count);
    }
}
