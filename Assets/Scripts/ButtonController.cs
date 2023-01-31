using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] bool isSoundClick = true;
    [SerializeField] bool isFillEffect = true;

    [SerializeField] Button button;

    [SerializeField] AudioSource soundClick;
    [SerializeField] GameObject fillEffect;


    void Start()
    {
        if (isFillEffect == true) { }; //test. has to be deleted

        if (isSoundClick)
        {
            button.onClick.AddListener(() => { soundClick.Play(); });
        }
    }
}
