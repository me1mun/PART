using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorCell : MonoBehaviour
{
    public Vector2Int position;
    public Element element;
    [SerializeField] private Image elementIcon;
    public LevelEditorField field;

    private void Start()
    {
        elementIcon.gameObject.SetActive(false);
    }

    public void SetElement()
    {
        element = field.activeElement;

        if (element.icon != null)
        {
            elementIcon.gameObject.SetActive(true);
            elementIcon.sprite = element.icon;
        }
        else
        {
            elementIcon.gameObject.SetActive(false);
        }

    }

}
