using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "Levels", order = 0)]
public class Level : ScriptableObject
{
    public string nameId;
    public int width;

    public enum Color { blue, red, green, yellow, purple };
    public Color color;

    public Part[] parts;
}

[Serializable]
public class Part
{
    public Element element;

    public bool isFixed = false;
    public int fixTurns;
}