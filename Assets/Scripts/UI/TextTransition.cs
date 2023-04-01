using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

public class TextTransition : MonoBehaviour
{
    private LocalizeStringEvent localizedStrinEvent;
    private CanvasGroup canvasGroup;

    private Coroutine coroutineTransition;
    [SerializeField] LocalizedString newStrnig;


    private void Awake()
    {
        localizedStrinEvent = GetComponent<LocalizeStringEvent>();
        canvasGroup = GetComponent<CanvasGroup>();

        localizedStrinEvent.StringReference = newStrnig;
    }

    private IEnumerator CoroutineTransition()
    {
        yield return null;
    }
}
