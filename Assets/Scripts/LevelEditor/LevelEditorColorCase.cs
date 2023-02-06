using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorColorCase : MonoBehaviour
{
    private LevelDatabase.Colors color;
    [SerializeField] private Image border, fill;

    [SerializeField] private Animator animator;

    public void Init(LevelDatabase.Colors col, Color32 col32)
    {
        color = col;
        //Color32 col32 = LevelDatabase.Instance.colorsList[color];

        border.color = col32;
        fill.color = col32;
    }

    public void SetupDisplay(LevelDatabase.Colors activeColor)
    {
        bool isActive = activeColor == color;

        if(isActive)
        {
            animator.SetTrigger("Activate");
        }
        else
        {
            animator.SetTrigger("Deactivate");
        }
    }

    public void SetPoolActiveColor()
    {
        LevelEditor.Instance.colorPool.SetActiveColor(color);
    }
}
