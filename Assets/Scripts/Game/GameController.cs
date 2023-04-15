using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class GameController : MonoBehaviour
{
    public enum GameStates { game, menu, victory }
    public GameStates gameState = GameStates.game;
    //private bool isPause = false;


    private RandomLevelGeneration randomLevelGenerator;
    [SerializeField] private AudioSource soundVictory;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private AnimationScale titleAnimation;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject infiniteIcon;
    [SerializeField] private TextTransition subtitle;
    [SerializeField] private LocalizedString subtitle_complete, subtitle_newLevels, subtitle_userLevels;
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
        if (Application.platform == RuntimePlatform.WindowsEditor && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CompleteLevel();
        }
    }

    public void SetGameStateGame()
    {
        gameState = GameStates.game;

        menu.Open(false);
        menu.SetInteractable(true);
        buttonNext.gameObject.SetActive(false);
        field.SetInteractable(true);
        titleAnimation.StartAnimationResize(1, 0.2f);
    }

    public void SetGameStateMenu()
    {
        gameState = GameStates.menu;

        menu.Open(true);
        menu.SetInteractable(true);
        buttonNext.gameObject.SetActive(false);
        field.SetInteractable(false);
        titleAnimation.StartAnimationResize(0.8f, 0.2f);
    }

    public void SetGameStateVictory()
    {
        gameState = GameStates.victory;

        menu.Open(false);
        menu.SetInteractable(false);
        buttonNext.gameObject.SetActive(true);
        buttonNext.ShowButton();
        field.SetInteractable(false);
        titleAnimation.StartAnimationResize(1, 0.2f);

        subtitle.StartTextTransition(subtitle_complete, -1f);
    }

    public void CompleteLevel()
    {
        LevelManager.Instance.UnlockChallenge(LevelManager.Instance.challengesUnlocked + 1);
        //LevelManager.Instance.SetLevel(LevelManager.Instance.level + 1);
        soundVictory.Play();
        SetGameStateVictory();
    }

    public void StartLevel(bool fadeOut = true)
    {
        level = LevelManager.Instance.GetLevel(LevelManager.Instance.gameMode, LevelManager.Instance.level);

        if (level.isRandom)
            level = randomLevelGenerator.GenerateLevel(level);

        if (fadeOut == false)
            canvasGroup.alpha = 0;

        if (coroutineLevelTransition != null)
        {
            StopCoroutine(coroutineLevelTransition);
        }
        coroutineLevelTransition = StartCoroutine(CoroutineLevelTransition());
    }

    public void CompleteNextButton()
    {
        //if()
    }

    public void StartNextLevel()
    {
        LevelManager.Instance.SetLevel(LevelManager.Instance.gameMode, LevelManager.Instance.level + 1);

        StartLevel();
    }

    public void StartRandomLevel()
    {
        int randomLevelIndex = Random.Range(0, LevelManager.Instance.GetLevelCount(LevelManager.Instance.gameMode));
        LevelManager.Instance.SetLevel(LevelManager.Instance.gameMode, randomLevelIndex);

        StartLevel();
    }

    private IEnumerator CoroutineLevelTransition()
    {
        float animTime = 0.2f;

        if (canvasGroup.alpha > 0)
            animationAlpha.StartAnimationAlpha(0, animTime);

        while (canvasGroup.alpha > 0)
            yield return null;

        //Debug.Log(level.isRandom);
        if (level.isRandom == false)
        {
            title.gameObject.SetActive(true);
            infiniteIcon.SetActive(false);
            title.text = "#" + (LevelManager.Instance.level + 1);
            subtitle.HideText();
        }
        else
        {
            title.gameObject.SetActive(false);
            infiniteIcon.SetActive(true);
            subtitle.SetText(subtitle_newLevels);
        }

        if (LevelManager.Instance.gameMode == LevelManager.GameModes.user)
        {
            subtitle.SetText(subtitle_userLevels);
        }

        field.CreateField(level);
        for(int i = 0; i < 10; i++)
        {
            if (field.CheckLoopComplete())
                field.CreateField(level);
        }

        // tutorial (first element in wrong position)
        //if (LevelManager.Instance.level == 0) 
        //   field.field[0, 0].FlipElement(4 + 1 - field.field[0, 0].flip, true);

        SetGameStateGame();

        animationAlpha.StartAnimationAlpha(1, animTime);
    }
}
