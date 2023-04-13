using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabLevelsCustomCase : MonoBehaviour
{
    public int index;

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
        index = scrollCase.GetIndex();
        //Debug.Log(LevelManager.Instance.GetLevelCount(LevelManager.GameModes.user) + " | " + index);
        level = LevelManager.Instance.GetLevel(LevelManager.GameModes.user, index);

        levelName.text = level.levelName;
    }

    public void InteractStart()
    {
        
        tabLevels.StartLevelFlash(index);
    }

    public void InteractDelete()
    {
        LevelManager.Instance.DeleteUserLevel(level.fileName);
        tabLevels.ReloadListCases();
    }
}
