using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PremiumManager : MonoBehaviour
{
    private string premiumProductId = "premium";
    private Product premiumProduct;
    private string saveKey = "premium";
    private SubscriptionManager subscriptionManager;

    public bool isPremium;

    private void Start()
    {
        IStoreController storeController = UnityEngine.Purchasing.CodelessIAPStoreListener.Instance.StoreController;
        premiumProduct = storeController.products.WithID(premiumProductId);

        LoadData();
    }

    private void Awake()
    {
        if (premiumProduct != null)
        {
            subscriptionManager = new SubscriptionManager(premiumProduct, null);
        }
    }

    private void LoadData()
    {
        int premiumInt = PlayerPrefs.GetInt(saveKey, 0);
        isPremium = premiumInt > 0;

        if (subscriptionManager != null && GameManager.CheckInternet())
        {
            isPremium = subscriptionManager.getSubscriptionInfo().isSubscribed() == Result.True;
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(saveKey, isPremium ? 1 : 0);
    }

    public void SetPremium(bool on)
    {
        isPremium = on;

        SaveData();
    }
}
