using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class GameController : MonoBehaviour
{
    private bool isPause = false;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI levelCounter;
    [SerializeField] private TextTransition subtitle;
    [SerializeField] private LocalizedString subtitle_complete, subtitle_newLevels;
    [SerializeField] private FieldController field;
    [SerializeField] private GameButtonComplete buttonNext;
    [SerializeField] private MenuBar menu;

    private AnimationAlpha animationAlpha;

    private Level level;

    //private float initProgress;
    private Coroutine coroutineLevelTransition;

    private void Awake()
    {
        animationAlpha = GetComponent<AnimationAlpha>();
        canvasGroup.alpha = 0;
    }

    void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        GameManager.UnlockLevel();
        GameManager.SetLevel(GameManager.level + 1);

        subtitle.StartTransition(subtitle_complete, -1f);
        field.SetInteractable(false);
        buttonNext.ShowButton();
        menu.SetInteractable(false);
        
    }

    public void StartLevel()
    {
        level = LevelList.Instance.GetLevel(GameManager.level);

        if (coroutineLevelTransition != null)
        {
            StopCoroutine(coroutineLevelTransition);
        }
        coroutineLevelTransition = StartCoroutine(CoroutineLevelTransition());
    }

    public void StartNextLevel()
    {
        StartLevel();
    }

    private IEnumerator CoroutineLevelTransition()
    {
        float animTime = 0.2f;

        animationAlpha.StartAnimationAlpha(0, animTime);

        while(canvasGroup.alpha > 0)
            yield return null;

        levelCounter.text = "#" + (GameManager.level + 1);
        subtitle.HideText();
        field.CreateField(level);
        field.SetInteractable(true);
        buttonNext.gameObject.SetActive(false);
        menu.SetInteractable(true);
        

        animationAlpha.StartAnimationAlpha(1, animTime);
    }
}
