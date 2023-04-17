using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabLevelsDeleteButton : MonoBehaviour
{
    [SerializeField] private ScrollUserLevelCase levelCase;

    private bool isReadyToDelete = false;
    private Coroutine coroutineDeleteConfirm;
    private float confirmTime = 3f;

    [SerializeField] private AnimationAlpha imageAnimation;
    [SerializeField] private Image image;
    [SerializeField] private Sprite iconDelete, iconConfirm;

    private void OnEnable()
    {
        SetupDefault();
    }

    private void SetupDefault()
    {
        isReadyToDelete = false;

        image.sprite = iconDelete;
        imageAnimation.SetAlpha(1);
    }


    public void InteractDelete()
    {
        if(isReadyToDelete)
        {
            levelCase.DeleteLevel();
        }
        else
        {
            StartDeleteConfirm();
        }
    }

    private void StartDeleteConfirm()
    {
        if (coroutineDeleteConfirm != null)
        {
            StopCoroutine(coroutineDeleteConfirm);
        }
        coroutineDeleteConfirm = StartCoroutine(CoroutineDeleteConfirm());
    }

    private IEnumerator CoroutineDeleteConfirm()
    {
        imageAnimation.StartAnimationAlpha(0, 0.2f);

        while (imageAnimation.GetAlpha() > 0)
            yield return null;

        image.sprite = iconConfirm;
        isReadyToDelete = true;

        imageAnimation.StartAnimationAlpha(1, 0.2f);

        yield return new WaitForSeconds(confirmTime);

        imageAnimation.StartAnimationAlpha(0, 0.2f);

        while (imageAnimation.GetAlpha() > 0)
            yield return null;

        image.sprite = iconDelete;
        isReadyToDelete = false;

        imageAnimation.StartAnimationAlpha(1, 0.2f);
    }
}
