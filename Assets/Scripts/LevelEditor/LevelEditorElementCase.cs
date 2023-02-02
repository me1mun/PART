using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorElementCase : MonoBehaviour
{
    [SerializeField] LevelEditorField field;

    public Element element;
    [SerializeField] private Image icon;

    void Start()
    {
        
    }

    public void Init(Element el)
    {
        element = el;

        icon.sprite = element.icon;
    }

    public void SetActiveElement()
    {
        field.SetActiveElement(element);
    }
}
