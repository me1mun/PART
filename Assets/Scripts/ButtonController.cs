using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    public bool interactable = true;

    private bool isPressed = false;
    private float animationProgress = 1;
    private Coroutine coroutinePressed;

    [SerializeField] private bool isSoundClick = true;
    [SerializeField] private bool isFillEffect = true;

    [SerializeField] private GameObject content;

    [SerializeField] private AudioSource soundClick;
    [SerializeField] private GameObject fillEffect;

    [Space(20)]
    public UnityEvent onClick;

    void Start()
    {
        if (isFillEffect == true) { }; //test. has to be deleted
    }

    private void Update()
    {
        if(isPressed)
        {
            if(Input.GetMouseButtonUp(0))
            {
                isPressed = false;
                AnimationPressed();
            }
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (interactable)
        {
            //Debug.Log("Button is clicked");
            onClick.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (interactable)
        {
            isPressed = true;
            AnimationPressed();

            if (isSoundClick)
            {
                soundClick.Play();
            }
        }
    }

    private IEnumerator CoroutinePressed()
    {
        //Debug.Log("Coroutine start");
        RectTransform contentRect = content.GetComponent<RectTransform>();
        CanvasGroup contentCanvas = content.GetComponent<CanvasGroup>();

        float targetProgress = isPressed ? 0 : 1;

        while (animationProgress != targetProgress)
        {
            animationProgress = Mathf.Lerp(animationProgress, targetProgress, 14 * Time.deltaTime);

            if (Mathf.Abs(animationProgress - targetProgress) < 0.02f)
            {
                animationProgress = targetProgress;
            }

            contentRect.localScale = Vector3.Lerp(new Vector3(0.84f, 0.84f, 1), new Vector3(1, 1, 1), animationProgress);
            contentCanvas.alpha = Mathf.Lerp(0.8f, 1, animationProgress);
            //Debug.Log("Coroutine: " + contentCanvas.alpha);

            yield return null;
        }
    }

    private void AnimationPressed()
    {
        if(coroutinePressed != null)
        {
            StopCoroutine(coroutinePressed);
        }

        coroutinePressed = StartCoroutine(CoroutinePressed());
    }

    public void SetInteractable(bool activate)
    {
        interactable = activate;
    }
}
