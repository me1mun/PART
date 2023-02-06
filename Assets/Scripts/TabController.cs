using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TabOpen()
    {
        gameObject.SetActive(true);
    }

    public void TabCloseAnimation()
    {
        animator.SetTrigger("close");
    }

    public void TabCloseEnd()
    {
        gameObject.SetActive(false);
    }
}
