using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class GameController : MonoBehaviour
{
    public enum GameStates { game, menu, victory }
    public GameStates gameState = GameStates.game;
    private bool isPause = false;


    private RandomLevelGeneration randomLevelGenerator;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI levelCounter;
    [SerializeField] private GameObject infiniteIcon;
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
        randomLevelGenerator = GetComponent<RandomLevelGeneration>();

        animationAlpha = GetComponent<AnimationAlpha>();
        canvasGroup.alpha = 0;
    }

    void Start()
    {
        field.OnLevelComplete.AddListener(CompleteLevel);
        StartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CompleteLevel();
        }
    }

    public void SetGameStateGame()
    {
        gameState = GameStates.game;

        menu.Activator(false);
        menu.SetInteractable(true);
        buttonNext.gameObject.SetActive(false);
        field.SetInteractable(true);
    }

    public void SetGameStateMenu()
    {
        gameState = GameStates.menu;

        menu.Activator(true);
        menu.SetInteractable(true);
        buttonNext.gameObject.SetActive(false);
        field.SetInteractable(false);
    }

    public void SetGameStateVictory()
    {
        gameState = GameStates.victory;

        menu.Activator(false);
        menu.SetInteractable(false);
        buttonNext.gameObject.SetActive(true);
        buttonNext.ShowButton();
        field.SetInteractable(false);

        subtitle.StartTextTransition(subtitle_complete, -1f);
    }

    public void CompleteLevel()
    {
        LevelManager.Instance.UnlockLevel();
        LevelManager.Instance.SetLevel(LevelManager.Instance.level + 1);

        SetGameStateVictory();
    }

    public void StartLevel(bool fadeOut = true)
    {
        level = LevelManager.Instance.GetLevel(LevelManager.Instance.level);

        if (level.levelType == LevelDatabase.LevelTypes.random)
            level = randomLevelGenerator.GenerateLevel(level);

        if (fadeOut == false)
            canvasGroup.alpha = 0;

        if (coroutineLevelTransition != null)
        {
            StopCoroutine(coroutineLevelTransition);
        }
        coroutineLevelTransition = StartCoroutine(CoroutineLevelTransition());
    }

    private IEnumerator CoroutineLevelTransition()
    {
        float animTime = 0.2f;

        if (canvasGroup.alpha > 0)
            animationAlpha.StartAnimationAlpha(0, animTime);

        while (canvasGroup.alpha > 0)
            yield return null;

        if (level.levelType == LevelDatabase.LevelTypes.challange)
        {
            levelCounter.gameObject.SetActive(true);
            infiniteIcon.SetActive(false);
            levelCounter.text = "#" + (LevelManager.Instance.level + 1);
            subtitle.HideText();
        }
        else if (level.levelType == LevelDatabase.LevelTypes.random)
        {
            levelCounter.gameObject.SetActive(false);
            infiniteIcon.SetActive(true);
            subtitle.SetText(subtitle_newLevels);
        }

        field.CreateField(level);

        if (LevelManager.Instance.level == 0)
            field.field[0, 0].FlipElement(4 - field.field[0, 0].flip, true);

        SetGameStateGame();

        animationAlpha.StartAnimationAlpha(1, animTime);
    }
}
