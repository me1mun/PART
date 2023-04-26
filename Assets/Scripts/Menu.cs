using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private TabManager tabManager;
    [SerializeField] private MenuBar menuBar;

    public void OpenTab(TabManager.TabEnum newTab)
    {
        menuBar.Open(true);
        tabManager.SwitchTab(newTab);
    }
}
