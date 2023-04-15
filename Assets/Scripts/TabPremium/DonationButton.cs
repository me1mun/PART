using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DonationButton : MonoBehaviour
{
    [SerializeField] string productId;
    [SerializeField] private GameObject price, notAvailable;
    [SerializeField] TextMeshProUGUI priceText;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        string loadedPrice = GameManager.Instance.storeManager.GetProductPrice(productId);
        bool priceIsAvailable = loadedPrice != "";

        price.SetActive(priceIsAvailable);
        priceText.text = GameManager.Instance.storeManager.GetProductPrice(productId);

        notAvailable.SetActive(!priceIsAvailable);
    }
}
