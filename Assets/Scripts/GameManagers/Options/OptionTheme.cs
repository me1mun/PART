using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionTheme : MonoBehaviour
{
    public  UnityEvent OnThemeChange;
    public enum ColorType { bg, overlay, content, accent };

    private string saveKey = "option_theme";
    private string defaultTheme = "default";
    private string themeName = "default";
    [SerializeField] private ColorTheme[] themeList;
    private List<string> themeNameList = new List<string>();
    private Dictionary<string, ColorTheme> nameThemeDict = new Dictionary<string, ColorTheme>();

    private void Awake()
    {
        LoadThemes();

        LoadData();
    }

    private void LoadThemes()
    {
        foreach (ColorTheme theme in themeList)
        {
            theme.colors[ColorType.bg] = theme.colorBg;
            theme.colors[ColorType.overlay] = theme.colorOverlay;
            theme.colors[ColorType.content] = theme.colorContent;
            theme.colors[ColorType.accent] = theme.colorAccent;

            nameThemeDict.Add(theme.themeName, theme);
            themeNameList.Add(theme.themeName);
        }
    }

    private void LoadData()
    {
        SetTheme(PlayerPrefs.GetString(saveKey, defaultTheme));
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(saveKey, themeName);
    }

    public void SetTheme(string newThemeName)
    {
        if (nameThemeDict.ContainsKey(newThemeName) == false)
        {
            newThemeName = defaultTheme;
        }

        themeName = newThemeName;
        OnThemeChange.Invoke();

        SaveData();
    }

    public void ChangeTheme()
    {
        int themeIndex = (FindThemeIndex() + 1) % themeNameList.Count;
        SetTheme(themeNameList[themeIndex]);
    }

    private int FindThemeIndex()
    {
        for (int i = 0; i < themeNameList.Count; i++)
            if (themeNameList[i] == themeName)
            {
                return i;
            }

        return 0;
    }

    public ColorTheme GetTheme()
    {
        return nameThemeDict[themeName];
    }
}

[System.Serializable]
public class ColorTheme
{
    public string themeName;

    public Color32 colorBg, colorOverlay, colorContent, colorAccent;

    public Dictionary<OptionTheme.ColorType, Color32> colors = new Dictionary<OptionTheme.ColorType, Color32>()
    {
        { OptionTheme.ColorType.bg, Color.black },
        { OptionTheme.ColorType.overlay, Color.grey },
        { OptionTheme.ColorType.content, Color.white },
        { OptionTheme.ColorType.accent, Color.blue }
    };
}