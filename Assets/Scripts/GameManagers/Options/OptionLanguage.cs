using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class OptionLanguage : MonoBehaviour
{
    private string saveKey = "option_language";
    private string defaultLanguage = "en";
    private string language = "en";
    private List<string> localeCodesList = new List<string>();
    private Dictionary<string, Locale> codeLocaleDict = new Dictionary<string, Locale>();

    private void Awake()
    {
        LoadLanguages();
        LoadData();
    }

    private void LoadLanguages()
    {
        foreach (Locale loc in LocalizationSettings.AvailableLocales.Locales)
        {
            codeLocaleDict.Add(loc.Identifier.Code, loc);
            localeCodesList.Add(loc.Identifier.Code);
        }

        defaultLanguage = LocalizationSettings.SelectedLocale.Identifier.Code;
    }

    private void LoadData()
    {

        SetLanguage(PlayerPrefs.GetString(saveKey, defaultLanguage));
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(saveKey, language);
    }

    public void SetLanguage(string newCode)
    {
        if (codeLocaleDict.ContainsKey(newCode) == false)
        {
            newCode = defaultLanguage;
        }

        language = newCode;
        LocalizationSettings.SelectedLocale = codeLocaleDict[newCode];

        SaveData();
    }

    public void ChangeLanguage()
    {
        int languageIndex = (FindLanguageIndex() + 1) % localeCodesList.Count;
        SetLanguage(localeCodesList[languageIndex]);
    }

    private int FindLanguageIndex()
    {
        for (int i = 0; i < localeCodesList.Count; i++)
            if (localeCodesList[i] == language)
            {
                return i;
            }

        return 0;
    }

    public string GetLanguageCode()
    {
        return language;
    }
}
