using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int level = 0;
    public static int levelLast = 1; // levelLast + 1 = real last

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    public static void ChangeLevel(int count)
    {
        level += count;
        level = Mathf.Clamp(level, 0, LevelData.levelsCount - 1);

        Debug.Log(level);
    }
}
