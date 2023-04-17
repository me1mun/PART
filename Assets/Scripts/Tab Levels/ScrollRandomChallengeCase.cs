using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRandomChallengeCase : MonoBehaviour 
{

    private TabLevels tabLevels;
    private InfiniteScrollCase scrollCase;

    [SerializeField] private GameObject outlineActive;

    private void Awake()
    {
        scrollCase = GetComponent<InfiniteScrollCase>();
        scrollCase.OnInit.AddListener(Init);
    }

    public void Init()
    {
        outlineActive.gameObject.SetActive(GameController.Instance.gameMode == LevelManager.GameModes.random);
    }

    public void Interact()
    {
        tabLevels = GetComponentInParent<TabLevels>();
        tabLevels.StartLevelFlash(LevelManager.GameModes.random, 0);
    }
}
