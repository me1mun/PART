using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBar : MonoBehaviour
{
    public bool isOpen = false;
    public bool isInteractable = true;

    [SerializeField] private TabManager tabManager;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject panel;
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
        Open(isOpen);
        SetInteractable(isInteractable);
    }

    public void Open(bool on)
    {
        isOpen = on;

        animator.SetBool("Activate", isOpen);
        gameField.SetInteractable(!isOpen);
        panel.gameObject.SetActive(isOpen);

        if (isOpen == false)
        {
            tabManager.CloseTabAll();
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
