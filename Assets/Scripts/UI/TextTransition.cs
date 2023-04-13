using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

[RequireComponent(typeof(AnimationAlpha), typeof(LocalizeStringEvent))]
public class TextTransition : MonoBehaviour
{
    [SerializeField] private LocalizeStringEvent localizedStrinEvent;
    [SerializeField] private CanvasGroup canvasGroup;

    private Coroutine coroutineTextTransition;

    [SerializeField] private AnimationAlpha animationAlpha;
    private LocalizedString stringReference;
    private float animTime = 0.2f;
    private const float defaultTextShowTime = 4.5f;
    private float textShowTime = 0;

    private void Awake()
    {
        localizedStrinEvent = GetComponent<LocalizeStringEvent>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        animationAlpha = GetComponent<AnimationAlpha>();
    }

    public void HideText()
    {
        gameObject.SetActive(false);
    }

    public void SetText(LocalizedString newText)
    {
        stringReference = newText;

        this.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        localizedStrinEvent.StringReference = stringReference;
    }

    public void StartTextTransition(LocalizedString newText, float newTextShowTime = defaultTextShowTime)
    {
        stringReference = newText;
        textShowTime = newTextShowTime;

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0;
        }

        if (coroutineTextTransition != null)
        {
            StopCoroutine(coroutineTextTransition);
        }
        coroutineTextTransition = StartCoroutine(CoroutineTextTransition());
    }

    private IEnumerator CoroutineTextTransition()
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
            yield break; //if show time <= 0 then end coroutine
        }

        animationAlpha.StartAnimationAlpha(0, animTime);

        while (canvasGroup.alpha > 0)
            yield return null;

        gameObject.SetActive(false);
    }
}
