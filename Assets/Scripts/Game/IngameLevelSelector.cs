using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IngameLevelSelector : MonoBehaviour
{
    private ButtonController buttonController;

    private bool isActive = false;
    private const float animationTime = 0.2f;

    [SerializeField] private AnimationAlpha buttonOutline;
    [SerializeField] private AnimationScale contentAnimator;

    private void Awake()
    {
        buttonController = GetComponent<ButtonController>();

        SetInteractable(false);
        buttonOutline.SetAlpha(0);
    }


    public void SetInteractable(bool on)
    {
        isActive = on;

        buttonController.SetInteractable(isActive);
        buttonOutline.StartAnimationAlpha(isActive ? 1f : 0f, animationTime);
        contentAnimator.StartAnimationResize(isActive ? 0.75f : 1f, animationTime);
    }
}
