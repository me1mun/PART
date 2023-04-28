using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public enum TabEnum { editor, levels, premium, options };
    private TabEnum currentTab;

    private TabController[] tabsArray;


    private void Awake()
    {
        tabsArray = GetComponentsInChildren<TabController>(true);
        //Debug.Log("tabs: " + tabsArray[0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl) && Application.platform == RuntimePlatform.WindowsEditor)
            SwitchTab(TabEnum.editor);
    }

    public void SwitchTab(TabEnum newTab)
    {
        currentTab = newTab;

        CloseTabAll();

        
        foreach (TabController tab in tabsArray)
        {
            //Debug.Log("Opent tab: ");
            if (tab.GetTabType() == newTab)
            {
                tab.Activate(true);
            }
        }
    }

    public void CloseTabAll()
    {
        foreach(TabController tab in tabsArray)
        {
            tab.Activate(false);
        }
    }
}
