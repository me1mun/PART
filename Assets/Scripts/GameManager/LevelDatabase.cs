using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDatabase : MonoBehaviour
{
    public static LevelDatabase Instance { get; private set; }

    public Element[] elements;
    public Element emptyElemet;

    public enum Colors { white, blue, purple, red, green };
    public Dictionary<Colors, Color32> colorsList = new Dictionary<Colors, Color32>();
    public Color32 defaultColor = new Color32(255, 255, 255, 255);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetupColors();
    }

    public Element GetElement(string elementName)
    {
        foreach(Element el in elements)
        {
            if (el.name == elementName)
            {
                return el;
            }
        }

        return emptyElemet;
    }

    private void SetupColors()
    {
        colorsList.Add(Colors.white, new Color32(255, 255, 255, 255));
        colorsList.Add(Colors.blue, new Color32(80, 140, 255, 255));
        colorsList.Add(Colors.purple, new Color32(160, 100, 250, 255));
        colorsList.Add(Colors.red, new Color32(255, 120, 130, 255));
        colorsList.Add(Colors.green, new Color32(90, 210, 100, 255));
    }

    public Color32 GetColor(Colors color)
    {
        if(colorsList.ContainsKey(color))
        {
            return colorsList[color];
        }

        return defaultColor;
    }
}
