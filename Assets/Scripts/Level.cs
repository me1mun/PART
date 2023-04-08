using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level
{
    public LevelDatabase.LevelTypes levelType = LevelDatabase.LevelTypes.challange;

    public string levelName;
    public int width, height;

    public string colorName = "";

    public string[] elements;
    public int[] elementFlip;
}
