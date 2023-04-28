using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private GameStateController gameStateController;

    public enum GameStates { game, menu, victory }
    private GameStates gameState;

    public int levelIndex = 0;
    private Level level;

    private GameUI gameUI;

    private RandomLevelGeneration randomLevelGenerator;
    [SerializeField] private AudioSource soundVictory;
    [SerializeField] TextTransition subtitle;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private FieldController field;
    [SerializeField] private MenuBar menu;

    private AnimationAlpha animationAlpha;

    //private float initProgress;
    private Coroutine coroutineLevelTransition;

    private void Awake()
    {
        Instance = this;

        gameStateController = GetComponent<GameStateController>();
        gameUI = GetComponent<GameUI>();

        randomLevelGenerator = GetComponent<RandomLevelGeneration>();

        animationAlpha = GetComponent<AnimationAlpha>();
        canvasGroup.alpha = 0;

        levelIndex = PlayerPrefs.GetInt("lastPlayedLevel", 0);
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

    public void SetLevel(int newLevelIndex)
    {
        if (newLevelIndex < LevelManager.Instance.GetLevelCount())
            levelIndex = newLevelIndex;

        PlayerPrefs.SetInt("lastPlayedLevel", levelIndex);
    }

    public void SetGameState(GameStates newState)
    {
        gameState = newState;

        gameStateController.SetGameStateDisplay(gameState);
    }

    public void SetGameStateMenu()
    {
        SetGameState(GameStates.menu);
    }

    public void SetGameStateGame()
    {
        SetGameState(GameStates.game);
    }

    public void CompleteLevel()
    {
        LevelManager.Instance.UnlockLevel(levelIndex + 2);
        soundVictory.Play();
        gameUI.SetSubtitleVictory();

        SetGameState(GameStates.victory);
    }

    public void StartLevel(bool fadeOut = true)
    {

        if (fadeOut == false)
            canvasGroup.alpha = 0;

        if (coroutineLevelTransition != null)
        {
            StopCoroutine(coroutineLevelTransition);
        }
        coroutineLevelTransition = StartCoroutine(CoroutineLevelTransition());
    }

    public void StartNextLevel()
    {
        SetLevel(levelIndex + 1);

        StartLevel();
    }

    private IEnumerator CoroutineLevelTransition()
    {
        level = LevelManager.Instance.GetLevel(levelIndex);

        if (level.isRandom)
            level = randomLevelGenerator.GenerateLevel();

        float animTime = 0.2f;

        if (canvasGroup.alpha > 0)
            animationAlpha.StartAnimationAlpha(0, animTime);

        while (canvasGroup.alpha > 0)
            yield return null;

        //Debug.Log(level.isRandom);

        gameUI.SetupDisplay(levelIndex, level);

        field.CreateField(level);
        for(int i = 0; i < 10; i++)
        {
            if (field.CheckLoopComplete())
                field.CreateField(level);
        }

        //if (LevelManager.Instance.level == 0) 
        //   subtitle.SetText(subtitleTutorial);

        SetGameState(GameStates.game);

        animationAlpha.StartAnimationAlpha(1, animTime);
    }
}

