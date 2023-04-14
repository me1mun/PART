using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AnimationAlpha : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private Coroutine coroutineAlpha;

    [SerializeField] private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private float animTime = 0.2f;
    private float targetAlpha = 1;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetAlpha(float newAlpha)
    {
        canvasGroup.alpha = newAlpha;
    }

    public float GetAlpha()
    {
        return canvasGroup.alpha;
    }

    public void StartAnimationAlpha(float newScale, float newTime)
    {
        animTime = newTime <= 0 ? 0.01f : newTime;
        targetAlpha = newScale;

        if (coroutineAlpha != null)
        {
            StopCoroutine(coroutineAlpha);
        }
        coroutineAlpha = StartCoroutine(CoroutineAlpha());
    }

    private IEnumerator CoroutineAlpha()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animTime)
        {
            float t = elapsedTime / animTime;
            float curveValue = animCurve.Evaluate(t);
            float newAlpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, curveValue);

            canvasGroup.alpha = newAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
