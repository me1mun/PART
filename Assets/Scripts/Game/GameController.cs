using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEditor.Localization.Editor;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private GameStateController gameStateController;

    public enum GameStates { game, menu, victory }
    public LevelManager.GameModes gameMode = LevelManager.GameModes.challenge;
    private Dictionary<LevelManager.GameModes, GameModeInfo> gmInfoDict = new Dictionary<LevelManager.GameModes, GameModeInfo>();
    [SerializeField] private List<GameModeInfo> gmInfoListTemp;

    public static GameStates gameState = GameStates.game;
    public int levelIndex = 0;

    private GameUI gameUI;

    private RandomLevelGeneration randomLevelGenerator;
    [SerializeField] private AudioSource soundVictory;
    [SerializeField] TextTransition subtitle;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private FieldController field;
    [SerializeField] private MenuBar menu;

    private AnimationAlpha animationAlpha;

    private Level level;

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

        foreach (GameModeInfo gmInfo in gmInfoListTemp)
        {
            gmInfo.levelsComplete = PlayerPrefs.GetInt(gmInfo.GetSaveKey(), 0);

            gmInfoDict.Add(gmInfo.gameMode, gmInfo);
            //Debug.Log($"{gmInfo.gameMode.ToString()}: {gmInfo.gameMode}");
        }
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

    public void SetLevel(LevelManager.GameModes newGameMode, int newLevelIndex)
    {
        gameMode = newGameMode;

        if (newLevelIndex > LevelManager.Instance.GetLevelCount(gameMode) - 1)
        {
            newLevelIndex = 0;
            if (gmInfoDict[gameMode].loopedSequence == false)
            {
                gameMode = LevelManager.GameModes.random;
            }
        }


        levelIndex = newLevelIndex;
    }

    public GameModeInfo GetGameModeInfo(LevelManager.GameModes gm)
    {
        return gmInfoDict[gm];
    }

    public int GetLevelsComplete(LevelManager.GameModes gm)
    {
        return gmInfoDict[gm].levelsComplete;
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
        UnlockLevel(levelIndex + 1);
        soundVictory.Play();
        gameUI.SetSubtitleVictory();

        SetGameState(GameStates.victory);
    }

    private void UnlockLevel(int newValue)
    {
        GameModeInfo gmInfo = gmInfoDict[gameMode];

        if (newValue > gmInfo.levelsComplete - 1)
        {
            gmInfo.levelsComplete = newValue;

            PlayerPrefs.SetInt(gmInfo.GetSaveKey(), gmInfo.levelsComplete);
        }
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
        SetLevel(gameMode, levelIndex + 1);

        StartLevel();
    }

    public void StartRandomLevel()
    {
        int randomLevelIndex = Random.Range(0, LevelManager.Instance.GetLevelCount(gameMode));
        SetLevel(gameMode, randomLevelIndex);

        StartLevel();
    }

    private IEnumerator CoroutineLevelTransition()
    {
        GameModeInfo gmInfo = gmInfoDict[gameMode];
        level = LevelManager.Instance.GetLevel(gameMode, levelIndex);

        if (gameMode == LevelManager.GameModes.random)
            level = randomLevelGenerator.GenerateLevel(level);

        float animTime = 0.2f;

        if (canvasGroup.alpha > 0)
            animationAlpha.StartAnimationAlpha(0, animTime);

        while (canvasGroup.alpha > 0)
            yield return null;

        //Debug.Log(level.isRandom);

        gameUI.SetupDisplay(gmInfo, levelIndex, level);

        field.CreateField(level);
        for(int i = 0; i < 10; i++)
        {
            if (field.CheckLoopComplete())
                field.CreateField(level);
        }

        // tutorial (first element in wrong position)
        //if (LevelManager.Instance.level == 0) 
        //   subtitle.SetText(subtitleTutorial);

        SetGameState(GameStates.game);

        animationAlpha.StartAnimationAlpha(1, animTime);
    }
}

