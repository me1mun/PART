using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using System;
using UnityEngine.Purchasing.Extension;

public class StoreManager : MonoBehaviour, IDetailedStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    private SubscriptionManager subscriptionManager;

    public string environment = "production";

    private void Awake()
    {
        
    }

    async void Start()
    {
        try
        {
            var options = new InitializationOptions()
                .SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
            // An error occurred during initialization.
        }

        StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.NotSpecified));
        if (Application.platform == RuntimePlatform.Android)
        {
            builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay));
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.AppleAppStore));
        }

        builder.AddProduct("donation_1", ProductType.Consumable);
        builder.AddProduct("donation_2", ProductType.Consumable);
        builder.AddProduct("donation_3", ProductType.Consumable);
        builder.AddProduct("premium", ProductType.Subscription);
        UnityPurchasing.Initialize(this, builder);

        subscriptionManager = new SubscriptionManager(storeController.products.WithID("premium"), null);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("IAP initialization failed: " + error);
    }

    public string GetProductPrice(string productId)
    {
        string priceString = "";

        if (storeController == null || extensionProvider == null)
        {
            Debug.Log("IAP is not initialized");
            return priceString;
        }

        Product product = storeController.products.WithID(productId);
        if (product != null && product.availableToPurchase)
        {
            priceString = product.metadata.localizedPriceString;
            //Debug.Log("The price for product " + productId + " is " + priceString);
        }
        else
        {
            //Debug.Log("Product not found or not available for purchase: " + productId);
        }

        return priceString;
    }

    public bool GetSubscriptionStatus()
    {
        return subscriptionManager.getSubscriptionInfo().isSubscribed() == Result.True;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new NotImplementedException();
    }

}
