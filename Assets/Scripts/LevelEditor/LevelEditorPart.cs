using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PartController))]
public class LevelEditorPart : MonoBehaviour
{
    private PartController part;

    void Awake()
    {
        part = GetComponent<PartController>();
    }

    public void Interact()
    {
        Element activeEl = LevelEditor.Instance.elementPool.GetActiveElement();


        if (activeEl.isEmpty || part.element.isEmpty)
        {
            part.Init(activeEl, 0);
        }
        else
        {
            part.FlipElement();
        }
    }
}
