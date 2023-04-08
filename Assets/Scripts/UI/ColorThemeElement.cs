using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorThemeElement : MonoBehaviour
{
    
    public ColorThemeManager.ColorType colorType;

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
        Color32 myColor = ColorThemeManager.Instance.GetColor(colorType);

        if (image != null)
            image.color = myColor;

        if (text != null)
            text.color = myColor;

        if (cam != null)
            cam.backgroundColor = myColor;
    }
}
