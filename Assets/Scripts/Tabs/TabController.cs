using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    private bool isActive = false;
    [SerializeField] private TabManager.TabEnum tabType;

    [SerializeField] private Animator animator;

    public void Activate(bool on)
    {
        isActive = on;

        if (on)
        {
            gameObject.SetActive(true);
            animator.SetBool("On", true);
        }
        else
        {
            if(gameObject.activeSelf)
                animator.SetBool("On", false);
        }
    }

    public void DeactivateEnd()
    {
        gameObject.SetActive(false);
    }

    public TabManager.TabEnum GetTabType()
    {
        return tabType;
    }
}
