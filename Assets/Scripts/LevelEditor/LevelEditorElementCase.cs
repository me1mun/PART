using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorElementCase : MonoBehaviour
{
    [SerializeField] LevelEditor levelEditor;

    public Element element;
    [SerializeField] private Image icon;
    //private Image iconImage;

    private void Awake()
    {
        //iconImage = icon.GetComponent<Image>();
    }

    public void Init(LevelEditor editor, Element el)
    {
        levelEditor = editor;

        element = el;

        icon.sprite = element.icon;
    }

    public void SetupDisplay(Element el)
    {
        if (element == el)
        {
            icon.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            icon.GetComponent<CanvasGroup>().alpha = 0.2f;
        }
    }

    public void SetPoolActiveElement()
    {
        levelEditor.elementPool.SetActiveElement(element);
    }
}
