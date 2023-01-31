using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private Level[] levelsTemp;

    public static Level[] levels;
    public static int levelsCount;

    private void Awake()
    {
        levels = levelsTemp;
        Debug.Log(levels[0]);
        levelsCount = levels.Length;
    }
}
