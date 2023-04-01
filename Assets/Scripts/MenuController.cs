using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] private Animator animator;
    [SerializeField] private FieldController gameField;

    void Start()
    {
        isOpen = false;
    }

    private void Update()
    {

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
}
