using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level
{
    public string nameId;
    public int width;

    public enum Color { blue, red, green, yellow, purple };
    public Color color;

    public Part[] parts;
}

//[Serializable]
public class Part
{
    public Element element;

    public int startTurns;
}