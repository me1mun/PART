using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public AudioManager audioManager { get; private set; }
    public GameManager gameManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        audioManager = GetComponentInChildren<AudioManager>();
        gameManager = GetComponentInChildren<GameManager>();
    }
}
