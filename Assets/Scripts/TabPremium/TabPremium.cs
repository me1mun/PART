using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
using UnityEngine.Purchasing;

public class TabPremium : MonoBehaviour
{
    private string premiumProductKey = "premium";

    [SerializeField] private ButtonController subscribeButton;
    [SerializeField] private LocalizeStringEvent subscribeButtonText;
    [SerializeField] private ThemeElement subscribeButtonTheme;
    [SerializeField] private ThemeElement premiumIconTheme;

    [SerializeField] LocalizedString stringSubscribe, stringSubscribed;

    private void Start()
    {
        SetupDisplay();
    }

    private void SetupDisplay()
    {
        if (GameManager.Instance.premiumManager.isPremium)
        {
            subscribeButton.SetInteractable(false);
            subscribeButtonText.StringReference = stringSubscribed;
            subscribeButtonTheme.SetColorType(OptionTheme.ColorType.overlay);
            premiumIconTheme.SetColorType(OptionTheme.ColorType.accent);
        }
        else
        {
            subscribeButton.SetInteractable(true);
            subscribeButtonText.StringReference = stringSubscribe;
            subscribeButtonTheme.SetColorType(OptionTheme.ColorType.accent);
            premiumIconTheme.SetColorType(OptionTheme.ColorType.content);

        }
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == premiumProductKey)
        {
            GameManager.Instance.premiumManager.SetPremium(true);
            SetupDisplay();

            Debug.Log("Premium sibscription is done");
        }

        Debug.Log(product.definition.id + " | " + premiumProductKey);
    }
} 
