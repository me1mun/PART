using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollUserLevelCase : MonoBehaviour
{
    public int levelIndex;

    private TabLevels tabLevels;
    private InfiniteScrollCase scrollCase;

    [SerializeField] private TextMeshProUGUI levelName;

    private Level level;

    private void Awake()
    {
        tabLevels = GetComponentInParent<TabLevels>();
        scrollCase = GetComponent<InfiniteScrollCase>();
        scrollCase.OnInit.AddListener(Init);
    }

    public void Init()
    {
        levelIndex = scrollCase.GetIndex();
        //Debug.Log(LevelManager.Instance.GetLevelCount(LevelManager.GameModes.user) + " | " + index);
        level = LevelManager.Instance.GetLevel(LevelManager.GameModes.user, levelIndex);

        levelName.text = level.levelName;
    }

    public void InteractStart()
    {
        
        tabLevels.StartLevelFlash(LevelManager.GameModes.user, levelIndex);
    }

    public void DeleteLevel()
    {
        LevelManager.Instance.DeleteUserLevel(level.levelName);
        tabLevels.ReloadListCases();
    }
}
