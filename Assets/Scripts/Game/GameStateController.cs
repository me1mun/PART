using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] private List<GameStateInfo> gameStatesList;
    private Dictionary<GameController.GameStates, GameStateInfo> gameStatesDict = new Dictionary<GameController.GameStates, GameStateInfo>();

    //display
    [SerializeField] private AnimationScale levelCounterScale;
    [SerializeField] private IngameLevelSelector levelSelector;
    [SerializeField] private FieldController field;
    [SerializeField] private GameObject victoryButtons;
    [SerializeField] private MenuBar menuBar;

    private void Awake()
    {
        foreach(GameStateInfo gsInfo in gameStatesList)
        {
            gameStatesDict.Add(gsInfo.gameState, gsInfo);
        }
    }

    public void SetGameStateDisplay(GameController.GameStates newState)
    {
        GameStateInfo stateInfo = gameStatesDict[newState];

        //display setup
        levelSelector.SetInteractable(stateInfo.levelSelectorIsActive);
        field.SetInteractable(stateInfo.fieldIsInteractable);
        menuBar.SetInteractable(stateInfo.menuIsInteractable);
        menuBar.Open(stateInfo.menuIsOpen);
        victoryButtons.SetActive(stateInfo.victoryButtonIsActive);
    }
}

[System.Serializable]
public class GameStateInfo
{
    public GameController.GameStates gameState;

    public bool menuIsOpen;
    public bool menuIsInteractable;
    public bool victoryButtonIsActive;
    public bool fieldIsInteractable;
    public bool levelSelectorIsActive;
}