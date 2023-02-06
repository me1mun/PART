using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameField field;
    [SerializeField] private TextMeshProUGUI levelCounter;

    //private float initProgress;
    private Coroutine levelinitCoroutine;

    void Start()
    {
        //initProgress = 0;
    }



    public void ChangeLevel(int count)
    {
        GameManager.ChangeLevel(count);
    }
}
