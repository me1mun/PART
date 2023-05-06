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

    [SerializeField] private ThemeElement premiumIconTheme;
    [SerializeField] private GameObject buttonSubscribe, buttonSubscribed;
    [SerializeField] private TextMeshProUGUI priceText;

    private void OnEnable()
    {
        SetupDisplay();
    }

    private void SetupDisplay()
    {
        bool isPremium = PremiumManager.isPremium;

        premiumIconTheme.SetColorType(isPremium ? OptionTheme.ColorType.accent : OptionTheme.ColorType.content);
        buttonSubscribe.SetActive(!isPremium);
        buttonSubscribed.SetActive(isPremium);

        string price = GameManager.Instance.storeManager.GetProductPrice("premium");

        if (price != "") 
            priceText.text = GameManager.Instance.storeManager.GetProductPrice("premium");
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "premium")
        {
            GameManager.Instance.premiumManager.SetPremium(true);
            SetupDisplay();

            //Debug.Log("Premium sibscription is done");
        }
    }
} 
