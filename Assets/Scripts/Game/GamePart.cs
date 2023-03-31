using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePart : MonoBehaviour
{
    private PartController part;

    void Awake()
    {
        part = GetComponent<PartController>();
    }

    public void Interact()
    {
        if (part.element.isFixed)
        {
            part.ShakeElement();
        }
        else
        {
            part.FlipElement();
        }
    }
}
