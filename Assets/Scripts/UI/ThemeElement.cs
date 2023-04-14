using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThemeElement : MonoBehaviour
{
    
    public OptionTheme.ColorType colorType;

    private Image image;
    private TextMeshProUGUI text;
    private Camera cam;

    void Awake()
    {
        if (GetComponent<Image>() != null)
            image = GetComponent<Image>();

        if (GetComponent<TextMeshProUGUI>() != null)
            text = GetComponent<TextMeshProUGUI>();

        if (GetComponent<Camera>() != null)
            cam = GetComponent<Camera>();

        GameManager.Instance.theme.OnThemeChange.AddListener(UpdateDisplayColor);
        UpdateDisplayColor();
    }

    public void UpdateDisplayColor()
    {
        Color32 myColor = GameManager.Instance.theme.GetTheme().colors[colorType];

        if (image != null)
            image.color = myColor;

        if (text != null)
            text.color = myColor;

        if (cam != null)
            cam.backgroundColor = myColor;
    }

    public void SetColorType(OptionTheme.ColorType newColor)
    {
        colorType = newColor;

        UpdateDisplayColor();
    }
}
