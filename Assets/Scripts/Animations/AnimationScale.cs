using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScale : MonoBehaviour
{
    private Coroutine coroutineResize;

    [SerializeField] private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private float animTime = 0.2f;
    private float targetScale = 1;

    public void StartAnimationResize(float newScale, float newTime)
    {
        animTime = newTime;
        targetScale = newScale;

        if (coroutineResize != null)
        {
            StopCoroutine(coroutineResize);
        }
        coroutineResize = StartCoroutine(CoroutineResize());
    }

    private IEnumerator CoroutineResize()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animTime)
        {
            float t = elapsedTime / animTime;
            float curveValue = animCurve.Evaluate(t);
            float newScale = Mathf.Lerp(transform.localScale.x, targetScale, curveValue); // Apply easing curve using Lerp

            transform.localScale = new Vector3(newScale, newScale, 1);
            //Debug.Log("New scale: " + newScale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set final size to target size to avoid precision errors
        transform.localScale = new Vector3(targetScale, targetScale, 1);
    }
}
