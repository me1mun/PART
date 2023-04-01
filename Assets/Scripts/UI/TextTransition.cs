using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

[RequireComponent(typeof(AnimationAlpha), typeof(LocalizeStringEvent))]
public class TextTransition : MonoBehaviour
{
    private LocalizeStringEvent localizedStrinEvent;
    private CanvasGroup canvasGroup;

    private Coroutine coroutineTransition;

    private AnimationAlpha animationAlpha;
    private LocalizedString stringReference;
    private float animTime = 0.33f;
    private const float defaultTextShowTime = 6f;
    private float textShowTime = 0;

    private void Awake()
    {
        localizedStrinEvent = GetComponent<LocalizeStringEvent>();
        canvasGroup = GetComponent<CanvasGroup>();

        animationAlpha = GetComponent<AnimationAlpha>();
    }

    public void StartTransition(LocalizedString newStrnig, float newTextShowTime = defaultTextShowTime)
    {
        stringReference = newStrnig;
        textShowTime = newTextShowTime;

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0;
        }

        if (coroutineTransition != null)
        {
            StopCoroutine(coroutineTransition);
        }
        coroutineTransition = StartCoroutine(CoroutineTransition());
    }

    private IEnumerator CoroutineTransition()
    {
        animationAlpha.StartAnimationAlpha(0, animTime); // alpha to 0

        while (canvasGroup.alpha > 0)
            yield return null;

        localizedStrinEvent.StringReference = stringReference; // set new string when alpha is 0

        animationAlpha.StartAnimationAlpha(1, animTime); // alpha to 1

        if (textShowTime > 0)
        {
            yield return new WaitForSeconds(textShowTime);
        }
        else
        {
            yield break;
        }

        animationAlpha.StartAnimationAlpha(0, animTime);
    }
}
