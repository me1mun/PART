using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButtonTheme : MonoBehaviour
{
    private void OnEnable()
    {
        SetupDisplay();
    }

    public void ChangeTheme()
    {
        GameManager.Instance.theme.ChangeTheme();

        SetupDisplay();
    }

    private void SetupDisplay()
    {
        //none
    }
}
