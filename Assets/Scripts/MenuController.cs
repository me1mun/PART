using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] private Animator animator;

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
            animator.SetTrigger("Open");
        }
        else
        {
            animator.SetTrigger("Close");
        }
    }
}
