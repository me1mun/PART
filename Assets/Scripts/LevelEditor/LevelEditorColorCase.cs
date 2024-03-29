using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorColorCase : MonoBehaviour
{
    [SerializeField] LevelEditor levelEditor;
    private LevelDatabase.Colors color;
    [SerializeField] private Image border, fill;

    [SerializeField] private AnimationScale animator;

    public void Init(LevelEditor editor, LevelDatabase.Colors col, Color32 col32)
    {
        levelEditor = editor;

        color = col;
        //Color32 col32 = LevelDatabase.Instance.colorsList[color];

        border.color = col32;
        fill.color = col32;
    }

    public void SetupDisplay(LevelDatabase.Colors activeColor)
    {
        float targetScale = activeColor == color ? 1f : 0f;

        animator.StartAnimationResize(targetScale, 0.25f);
    }

    public void SetPoolActiveColor()
    {
        levelEditor.colorPool.SetActiveColor(color);
    }
}
