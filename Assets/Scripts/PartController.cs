using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartController : MonoBehaviour
{
    private ButtonController buttonController;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject content;

    public Element.ConnectionTypes connectionLeft = Element.ConnectionTypes.none;
    public Element.ConnectionTypes connectionUp = Element.ConnectionTypes.none;
    public Element.ConnectionTypes connectionRight = Element.ConnectionTypes.none;
    public Element.ConnectionTypes connectionDown = Element.ConnectionTypes.none;

    private Vector2Int position;
    public bool isFixed = false;
    public Element element;
    private Color32 color;
    public int flip = 0;
    private float targetAngle = 0;

    private Coroutine coroutineAnimation;

    private void Awake()
    {
        buttonController = GetComponent<ButtonController>();
        //Init(LevelDatabase.Instance.emptyElemet, LevelDatabase.Instance.defaultColor, 0);
    }

    public void Init(Element el, int flipCount, Vector2Int pos)
    {
        element = el;
        isFixed = el.isFixed;
        position = pos;

        if (el.isEmpty == false) 
        {
            icon.enabled = true;
            icon.sprite = el.icon;
        }
        else
        {
            icon.enabled = false;
            //GetComponent<ButtonController>().SetInteractable(false);
        }

        connectionLeft = el.connectionLeft;
        connectionUp = el.connectionUp;
        connectionRight = el.connectionRight;
        connectionDown = el.connectionDown;

        FlipElement(flipCount, true);
    }

    public void Interact()
    {
        if(!isFixed)
        {
            //FlipElement();
        }
        else
        {
            //AnimationStart("CoroutineShake");
        }
    }

    public void PaintSelf(Color32 col32)
    {
        icon.color = col32;
    }

    public void FlipElement(int turnCount = 1, bool flashTurn = false)
    {
        //Debug.Log("Flip method");
        for (int i = 0; i < turnCount; i++)
        {
            flip += 1;
            targetAngle = flip * -90;

            ConnectionsShift();

            if (flashTurn)
            {
                content.transform.localRotation = Quaternion.Euler(0, 0, targetAngle);
                targetAngle = content.transform.localRotation.eulerAngles.z;
            }
            else
            {
                AnimationStart("CoroutineFlip");
            }
        }
    }

    public void ShakeElement()
    {
        AnimationStart("CoroutineShake");
    }

    private void ConnectionsShift()
    {
        Element.ConnectionTypes tempConnection = connectionLeft;

        connectionLeft = connectionDown;
        connectionDown = connectionRight;
        connectionRight = connectionUp;
        connectionUp = tempConnection;
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    private void AnimationStart(string coroutine)
    {
        if (coroutineAnimation != null)
            StopCoroutine(coroutineAnimation);

        coroutineAnimation = StartCoroutine(coroutine);
    }

    private IEnumerator CoroutineShake()
    {
        float[] gaps = new float[5] { -14, 9, -5, 2, 0 };

        for (int i = 0; i < gaps.Length; i++)
        {
            Quaternion rotationTarget = Quaternion.Euler(0, 0, flip * -90 + gaps[i]);
            //float shakeTarget = targetAngle + gaps[i];

            while (Mathf.Round(content.transform.localRotation.eulerAngles.z) != (rotationTarget.eulerAngles.z))
            {
                content.transform.localRotation = Quaternion.Lerp(content.transform.localRotation, rotationTarget, 20f * Time.deltaTime);
                yield return null;
            }
        }
    }

    private IEnumerator CoroutineFlip()
    {
        //Debug.Log("Flip coroutine");
        Quaternion rotationTarget = Quaternion.Euler(0, 0, flip * -90);

        while (Mathf.Round(content.transform.localRotation.eulerAngles.z) != (rotationTarget.eulerAngles.z))
        {
            content.transform.localRotation = Quaternion.Lerp(content.transform.localRotation, rotationTarget, 14f * Time.deltaTime);
            yield return null;
        }

        content.transform.localRotation = rotationTarget;
    }

    public void SetInteractable(bool interactable)
    {
        buttonController.SetInteractable(interactable);
    }
}
