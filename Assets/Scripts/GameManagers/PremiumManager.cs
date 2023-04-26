using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PremiumManager : MonoBehaviour
{
    private string saveKey = "premium";

    public static bool isPremium;

    private void Start()
    {

        LoadData();
    }

    private void Awake()
    {

    }

    private void LoadData()
    {
        StoreManager store = GameManager.Instance.storeManager;

        int premiumInt = PlayerPrefs.GetInt(saveKey, 0);
        isPremium = premiumInt > 0;

        if (GameManager.CheckInternet() && GameManager.IsMobilePlatform())
        {
            SetPremium(store.GetSubscriptionStatus());
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
