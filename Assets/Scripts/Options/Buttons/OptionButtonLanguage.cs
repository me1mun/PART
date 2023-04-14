using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionButtonLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI languageCode;

    private void OnEnable()
    {
        SetupDisplay();
    }

    public void ChangeLanguage()
    {
        GameManager.Instance.language.ChangeLanguage();

        SetupDisplay();
    }

    private void SetupDisplay()
    {
        languageCode.text = GameManager.Instance.language.GetLanguageCode().ToUpper();
        //Debug.Log(OptionsManager.Instance.GetLanguageCode().ToUpper());
    }
}
