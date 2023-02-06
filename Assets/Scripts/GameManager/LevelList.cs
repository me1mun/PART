using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelList : MonoBehaviour
{
    public static LevelList Instance { get; private set; }

    public List<TextAsset> levelJson = new List<TextAsset>();

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
    }

    void Start()
    {
        
    }
}
