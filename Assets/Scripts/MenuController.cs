using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public bool isOpen = false;
    public bool isInteractable = true;
    [SerializeField] private Animator animator;
    [SerializeField] private FieldController gameField;
    [SerializeField] private ButtonController[] buttons;

    private AnimationAnchor animationAnchor;
    private Vector2 startAnchorPos;

    private void Awake()
    {
        buttons = GetComponentsInChildren<ButtonController>(true);
        animationAnchor = GetComponent<AnimationAnchor>();

        startAnchorPos = GetComponent<RectTransform>().anchoredPosition;
    }

    private void Start()
    {
        Activator(isOpen);
        SetInteractable(isInteractable);
    }

    public void Activator(bool activate)
    {
        isOpen = activate;

        if (isOpen)
        {
            animator.SetBool("Activate", true);
            gameField.SetInteractable(false);
        }
        else
        {
            animator.SetBool("Activate", false);
            gameField.SetInteractable(true);
        }
    }

    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable;

        float newY = isInteractable ? startAnchorPos.y : -100f;
        Vector2 newPos = new Vector2(0, newY);
        animationAnchor.StartAnimationMove(newPos, 0.2f);

        foreach (ButtonController btn in buttons)
        {
            btn.SetInteractable(interactable);
        }
    }
}
