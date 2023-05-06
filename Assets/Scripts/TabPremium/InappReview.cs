using Google.Play.Review;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InappReview : MonoBehaviour
{
    private ReviewManager _reviewManager = new ReviewManager();
    private PlayReviewInfo _playReviewInfo;

    private Coroutine reviewCoroutine;

    public void ShowReview()
    {
        _reviewManager = new ReviewManager();

        if (reviewCoroutine != null)
            StopCoroutine(reviewCoroutine);

        reviewCoroutine = StartCoroutine(ReviewCoroutine());
    }

    private IEnumerator ReviewCoroutine()
    {
        Debug.Log("InApp review start");

        yield return new WaitForSeconds(1f);

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.LogWarning("Request #1: " + requestFlowOperation.Error.ToString());
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();


        while (_playReviewInfo == null)
            yield return null;


        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object

        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.LogWarning("Request #2: " + requestFlowOperation.Error.ToString());
            yield break;
        }

        Debug.Log("InApp review finish");
    }
}
