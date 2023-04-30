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
        if (reviewCoroutine != null)
            StopCoroutine(reviewCoroutine);

        reviewCoroutine = StartCoroutine(ReviewCoroutine());
    }

    private IEnumerator ReviewCoroutine()
    {
        Debug.Log("InApp review show");

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.LogWarning(requestFlowOperation.Error.ToString());
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
    }
}
