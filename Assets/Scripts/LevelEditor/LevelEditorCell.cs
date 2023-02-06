using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorCell : MonoBehaviour
{
        [Header("Initialization")]
    [SerializeField] private GameObject content;
    [SerializeField] private Image elementIcon;

        [Header("Properties")]
    public Element element;
    private Color32 color;
    public int elementFlip = 0;
    private Coroutine coroutineFlip;

    private void Start()
    {
        SetupDisplay();
    }


    public void SetupDisplay()
    {

        if (element != null)
        {
            content.SetActive(true);

            elementIcon.sprite = element.icon;

        }
        else
        {
            content.SetActive(false);
        }
    }

    public void PaintSelf(Color32 newColor)
    {
        color = newColor;

        elementIcon.color = color;
    }

    public void Interact()
    {
        if (element == null || LevelEditor.Instance.elementPool.GetActiveElement() == null)
        {
            InteractSetElement();
        }
        else
        {
            InteractFlipElement();
        }
    }

    private void InteractFlipElement()
    {
        elementFlip = elementFlip == 3 ? 0 : elementFlip + 1;
        //elementIcon.gameObject.transform.localRotation = Quaternion.Euler(0, 0, elementFlip * -90);

        AnimationFlip();
    }

    private void InteractSetElement()
    {
        element = LevelEditor.Instance.elementPool.GetActiveElement();
        SetupDisplay();
    }

    private void AnimationFlip()
    {
        if (coroutineFlip != null)
            StopCoroutine(coroutineFlip);

        coroutineFlip = StartCoroutine(CoroutineFlip());
    }

    private IEnumerator CoroutineFlip()
    {
        Quaternion rotationTarget = Quaternion.Euler(0, 0, elementFlip * -90);

        while (Mathf.Round(content.transform.localRotation.eulerAngles.z) != (rotationTarget.eulerAngles.z))
        {
            content.transform.localRotation = Quaternion.Lerp(content.transform.localRotation, rotationTarget, 14f * Time.deltaTime);
            yield return null;
        }

        content.transform.localRotation = rotationTarget;
    }
}
