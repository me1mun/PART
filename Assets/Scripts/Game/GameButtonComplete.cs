using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonComplete : MonoBehaviour
{
    private AnimationScale animationScale;
    //private ButtonController button;

    //public bool isInteractable = false;

    private void Awake()
    {
        animationScale = GetComponent<AnimationScale>();
        //button = GetComponent<ButtonController>();
    }

    private void Start()
    {
        //ShowButton();
    }

    public void ShowButton()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;

        animationScale.StartAnimationResize(1, 0.2f);
    }
}
