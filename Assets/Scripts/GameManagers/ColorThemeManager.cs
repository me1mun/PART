using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ColorThemeManager : MonoBehaviour
{
    public static ColorThemeManager Instance { get; private set; }

    public UnityEvent onColorThemeChange;

    public enum ThemeType { darkDefault, white };
    public enum ColorType { bg, overlay, content, accent };
    [SerializeField] private ColorTheme[] colorThemeArary;
    private Dictionary<ThemeType, ColorTheme> colorThemeDict = new Dictionary<ThemeType, ColorTheme>();

    private ThemeType currentTheme = ThemeType.darkDefault;
    public Dictionary<ColorType, Color32> Colors = new Dictionary<ColorType, Color32>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        InitThemes();

        SetColorTheme(currentTheme);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ChangeColorTheme();
        }
    }

    private void InitThemes()
    {
        foreach(ColorType colType in Enum.GetValues(typeof(ColorType)))
        {
            Colors.Add(colType, new Color32(255, 0, 0, 255));
        }

        foreach (ColorTheme ct in colorThemeArary)
        {
            colorThemeDict.Add(ct.themeName, ct);
        }

    }

    public void SetColorTheme(ThemeType newTheme) 
    {
        currentTheme = newTheme;

        if(colorThemeDict.ContainsKey(newTheme))
        {
            Colors[ColorType.bg] = colorThemeDict[newTheme].colorBg;
            Colors[ColorType.overlay] = colorThemeDict[newTheme].colorOverlay;
            Colors[ColorType.content] = colorThemeDict[newTheme].colorContent;
            Colors[ColorType.accent] = colorThemeDict[newTheme].colorAccent;
        }
        else if (colorThemeDict.ContainsKey(ThemeType.darkDefault))
        {
            SetColorTheme(ThemeType.darkDefault);
        }

        onColorThemeChange.Invoke();
    }

    public void ChangeColorTheme()
    {
        SetColorTheme(currentTheme == ThemeType.white ? ThemeType.darkDefault : ThemeType.white);
        Debug.Log("Change color theme");
    }

    public Color32 GetColor(ColorType colType)
    {
        return Colors[colType];
    }
}


[System.Serializable]
public class ColorTheme
{
    public ColorThemeManager.ThemeType themeName;

    public Color32 colorBg, colorOverlay, colorContent, colorAccent;
}
