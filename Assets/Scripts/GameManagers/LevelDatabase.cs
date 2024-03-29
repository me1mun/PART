using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDatabase : MonoBehaviour
{
    public static LevelDatabase Instance { get; private set; }

    public Element[] elements;
    public Element emptyElemet, universalElement;

    public enum Colors { red, yellow, green, blue, purple };
    public Dictionary<Colors, Color32> colorsList = new Dictionary<Colors, Color32>();
    public Colors defaultColor = Colors.blue;

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
        Element resultElement = universalElement;

        if (elementName == "")
        {
            resultElement = emptyElemet;
        }
        else
        {
            foreach (Element el in elements)
            {
                if (el.name == elementName)
                {
                    resultElement = el;
                }
            }
        }

        return resultElement;
    }

    private void SetupColors()
    {
        colorsList.Add(Colors.red, new Color32(255, 120, 130, 255));
        colorsList.Add(Colors.yellow, new Color32(252, 205, 83, 255));
        colorsList.Add(Colors.green, new Color32(142, 210, 100, 255));
        colorsList.Add(Colors.blue, new Color32(80, 140, 255, 255));
        colorsList.Add(Colors.purple, new Color32(160, 100, 250, 255));
    }

    public Colors GetColorEnum(string colorName)
    {
        Colors result = defaultColor;

        if (System.Enum.IsDefined(typeof(LevelDatabase.Colors), colorName))
            result = System.Enum.Parse<LevelDatabase.Colors>(colorName);

        return result;
    }

    public Color32 GetColor(Colors color)
    {
        if(colorsList.ContainsKey(color))
        {
            return colorsList[color];
        }

        return colorsList[defaultColor];
    }
}
