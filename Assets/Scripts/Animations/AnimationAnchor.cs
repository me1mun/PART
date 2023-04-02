using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAnchor : MonoBehaviour
{
    private RectTransform rectTransform;

    private Coroutine coroutineMove;

    [SerializeField] private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private float animTime = 0.2f;
    private Vector3 targetPosition = Vector3.zero;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void StartAnimationMove(Vector2 newPosition, float newTime)
    {
        animTime = newTime;
        targetPosition = newPosition;

        if (coroutineMove != null)
        {
            StopCoroutine(coroutineMove);
        }
        coroutineMove = StartCoroutine(CoroutineMove());
    }

    private IEnumerator CoroutineMove()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animTime)
        {
            float t = elapsedTime / animTime;
            float curveValue = animCurve.Evaluate(t);
            Vector2 newPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, curveValue);

            rectTransform.anchoredPosition = newPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }

    private void OnDestroy()
    {
        if (coroutineMove != null)
        {
            StopCoroutine(coroutineMove);
        }
    }
}
