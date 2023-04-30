using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePart : MonoBehaviour
{
    private FieldController field;
    private PartController part;

    void Awake()
    {
        field = transform.parent.GetComponent<FieldController>();
        part = GetComponent<PartController>();
    }

    public void Interact()
    {
        if (field.isInteractable)
        {
            if (part.element.isFixed)
            {
                part.ShakeElement();
            }
            else
            {
                part.FlipElement();
                field.CheckLevelComplete();
            }
        }
    }
}
