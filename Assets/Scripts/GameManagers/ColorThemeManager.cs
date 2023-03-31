using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorThemeManager : MonoBehaviour
{
    public static ColorThemeManager Instance { get; private set; }

    public UnityEvent onColorThemeChange;

    public enum Theme { darkDefault, white };
    private Dictionary<Theme, ColorTheme> colorThemeDict = new Dictionary<Theme, ColorTheme>();

    private Theme currentTheme = Theme.darkDefault;
    public Color32 colorBg, colorOverlay, colorContent, colorAccent;

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
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeColorTheme();
        }
    }

    private void InitThemes()
    {
        colorThemeDict.Add(Theme.darkDefault, new ColorTheme(new Color32(35, 35, 52, 255), new Color32(46, 46, 69, 255), new Color32(255, 255, 255, 255), new Color32(80, 140, 255, 255))); // default
        colorThemeDict.Add(Theme.white, new ColorTheme(new Color32(242, 242, 242, 255), new Color32(255, 255, 255, 255), new Color32(46, 46, 69, 255), new Color32(80, 140, 255, 255)));
    }

    public void SetColorTheme(Theme newTheme) 
    {
        currentTheme = newTheme;

        if(colorThemeDict.ContainsKey(newTheme))
        {
            colorBg = colorThemeDict[newTheme].colorBg;
            colorOverlay = colorThemeDict[newTheme].colorOverlay;
            colorContent = colorThemeDict[newTheme].colorContent;
            colorAccent = colorThemeDict[newTheme].colorAccent;
        }
        else if (colorThemeDict.ContainsKey(Theme.darkDefault))
        {
            SetColorTheme(Theme.darkDefault);
        }

        onColorThemeChange.Invoke();
    }

    public void ChangeColorTheme()
    {
        SetColorTheme(Theme.white);
        Debug.Log("Change Theme to white");
    }
}

public class ColorTheme
{
    public Color32 colorBg, colorOverlay, colorContent, colorAccent;

    public ColorTheme(Color32 colBg, Color32 colOverlay, Color32 colContent, Color32 colAccent)
    {
        colorBg = colBg;
        colorOverlay = colOverlay;
        colorContent = colContent;
        colorAccent = colAccent;
    }

}
