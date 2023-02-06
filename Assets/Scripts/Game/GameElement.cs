using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameElement : MonoBehaviour
{
    [SerializeField] private GameObject icon;

    public Element.ConnectionTypes connectionLeft = Element.ConnectionTypes.none;
    public Element.ConnectionTypes connectionUp = Element.ConnectionTypes.none;
    public Element.ConnectionTypes connectionRight = Element.ConnectionTypes.none;
    public Element.ConnectionTypes connectionDown = Element.ConnectionTypes.none;

    public bool isFixed = false;

    private float targetAngle;

    private Coroutine coroutineShake;

    void Awake()
    {

    }

    private void Update()
    {
        if(!isFixed && icon.transform.localRotation.eulerAngles.z != targetAngle)
        {
            icon.transform.localRotation = Quaternion.Lerp(icon.transform.localRotation, Quaternion.Euler(0, 0, targetAngle), 14f * Time.deltaTime);
        }
    }

    public void Init(Element el, Color32 color, int flipCount)
    {
        isFixed = el.isFixed;
        Image image = icon.GetComponent<Image>();

        if (el.name != "_Empty") 
        { 
            image.sprite = el.icon;
            image.color = color;
        }
        else
        {
            image.enabled = false;
        }

        connectionLeft = el.connectionLeft;
        connectionUp = el.connectionUp;
        connectionRight = el.connectionRight;
        connectionDown = el.connectionDown;
        
        ElementTurn(flipCount, true);
    }

    public void Interact()
    {
        if(!isFixed)
        {
            ElementTurn();
        }
        else
        {
            AnimationShake();
        }
    }

    public void ElementTurn(int turnCount = 1, bool flashTurn = false)
    {
        for(int i = 0; i < turnCount; i++)
        {
            targetAngle -= 90;

            ConnectionsShift();

            if (flashTurn)
            {
                icon.transform.localRotation = Quaternion.Euler(0, 0, targetAngle);
                targetAngle = icon.transform.localRotation.eulerAngles.z;
            }
        }
    }

    private IEnumerator CoroutineShake()
    {
        //transform.localRotation = Quaternion.Euler(0, 0, targetAngle);
        float startAngle = targetAngle;

        for (int i = 0; i <= 5; i++)
        {
            float[] gaps = new float[6] { -14, 10, -7, 5, -4, 0 };
            float shakeTarget = startAngle + gaps[i];

            while (Mathf.Round(icon.transform.localRotation.eulerAngles.z) != Mathf.Round(shakeTarget))
            {
                icon.transform.localRotation = Quaternion.Lerp(icon.transform.localRotation, Quaternion.Euler(0, 0, shakeTarget), 14f * Time.deltaTime);
                yield return null;
            }
        }

        yield break;
    }

    private void AnimationShake()
    {
        if (coroutineShake != null)
        {
            StopCoroutine(coroutineShake);
        }

        coroutineShake = StartCoroutine(CoroutineShake());
    }

    private void ConnectionsShift()
    {
        Element.ConnectionTypes tempConnection = connectionLeft;

        connectionLeft = connectionDown;
        connectionDown = connectionRight;
        connectionRight = connectionUp;
        connectionUp = tempConnection;
    }
}
