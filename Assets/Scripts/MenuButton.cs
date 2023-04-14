using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    //private bool isActive = false;
    private Menu menu;

    [SerializeField] TabManager.TabEnum tabType;
    [SerializeField] Image icon;

    private void Awake()
    {
        menu = GetComponentInParent<Menu>();
    }

    public void Interact()
    {
        menu.OpenTab(tabType);
    }

    public void Activate(bool on)
    {

    }
}
