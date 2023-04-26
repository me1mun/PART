using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public AudioManager audioManager;
    public OptionTheme theme;
    public OptionOrientation orientation;
    public OptionLanguage language;
    public PremiumManager premiumManager;
    public StoreManager storeManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public static bool CheckInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    public static bool IsMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }
}
