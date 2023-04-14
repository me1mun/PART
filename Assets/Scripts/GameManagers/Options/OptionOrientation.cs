using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionOrientation : MonoBehaviour
{
    private string saveKey = "option_rientation";
    private int defaultOrientation = 0;
    private int currentOrientation = 0;
    [SerializeField] private Orientation[] orientationsArray;

    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            currentOrientation = PlayerPrefs.GetInt(saveKey);
        }
        else
        {
            currentOrientation = defaultOrientation;
            SaveData();
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(saveKey, currentOrientation);
    }

    public void SetOrientation(int newOrientation)
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            currentOrientation = newOrientation % orientationsArray.Length;

            Screen.orientation = orientationsArray[currentOrientation].screenOrientation;

            SaveData();
        }
    }

    public int GetCurrentOrientation()
    {
        return currentOrientation;
    }

    public Sprite GetCurrentOrientationIcon()
    {
        return orientationsArray[currentOrientation].icon;
    }
}

[System.Serializable]
public class Orientation
{
    public ScreenOrientation screenOrientation;
    public Sprite icon;
}