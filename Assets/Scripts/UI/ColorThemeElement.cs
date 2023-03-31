using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorThemeElement : MonoBehaviour
{
    public enum ColorType { bg, overlay, content, accent };
    public ColorType colorType;

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

        ColorThemeManager.Instance.onColorThemeChange.AddListener(UpdateColor);
        UpdateColor();
    }

    public void UpdateColor()
    {
        Color32 myColor = ColorThemeManager.Instance.colorOverlay;

        switch(colorType)
        {
            case ColorType.bg:
                myColor = ColorThemeManager.Instance.colorBg;
                break;
            case ColorType.overlay:
                myColor = ColorThemeManager.Instance.colorOverlay;
                break;
            case ColorType.content:
                myColor = ColorThemeManager.Instance.colorContent;
                break;
            case ColorType.accent:
                myColor = ColorThemeManager.Instance.colorAccent;
                break;
        }

        if (image != null)
            image.color = myColor;

        if (text != null)
            text.color = myColor;

        if (cam != null)
            cam.backgroundColor = myColor;
    }
}
