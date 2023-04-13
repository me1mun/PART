using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level
{
    public string fileName;
    public bool isRandom = false;

    public string levelName;
    public int width, height;

    public string colorName = "";

    public string[] elements;
    public int[] elementFlip;
}
