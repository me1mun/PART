using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PartController))]
public class LevelEditorPart : MonoBehaviour
{
    private LevelEditor levelEditor;
    private PartController partController;

    void Awake()
    {
        levelEditor = GetComponentInParent<LevelEditor>();

        partController = GetComponent<PartController>();
    }

    public void Interact()
    {
        Element activeEl = levelEditor.elementPool.GetActiveElement();


        if (activeEl.isEmpty || partController.element.isEmpty)
        {
            partController.Init(activeEl, 0, partController.GetPosition());
        }
        else
        {
            partController.FlipElement();
        }
    }
}
