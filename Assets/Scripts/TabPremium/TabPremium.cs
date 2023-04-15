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
    [SerializeField] private List<string> donationIdList;

    [SerializeField] private ThemeElement premiumIconTheme;


    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if (GameManager.Instance.premiumManager.isPremium)
        {
            premiumIconTheme.SetColorType(OptionTheme.ColorType.accent);
        }
        else
        {
            premiumIconTheme.SetColorType(OptionTheme.ColorType.content);

        }
    }

    public void OnPurchaseComplete(Product product)
    {
        if (donationIdList.Contains(product.definition.id))
        {
            GameManager.Instance.premiumManager.SetPremium(true);
            Setup();

            //Debug.Log("Premium sibscription is done");
        }

        Debug.Log(product.definition.id + ": try to buy");
    }
} 
