using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElementController : MonoBehaviour, IPointerDownHandler
{
    private Image image;

    public Element.ConnectionTypes[] connections;

    private bool isFixed = false;
    private float targetAngle;

    private Coroutine coroutineShake;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(!isFixed && transform.localRotation.eulerAngles.z != targetAngle)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, targetAngle), 14f * Time.deltaTime);
            //transform.localRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90 * Time.deltaTime);
            //Debug.Log(transform.eulerAngles.z);
            //transform.Rotate(transform.eulerAngles, );
        }
    }

    public void Init(Part part, Level level)
    {
        //isFixed = part.isFixed;

        if (part.element.icon != null) 
        { 
            image.sprite = part.element.icon;
            //image.color = level.color;
        }
        else
        {
            image.enabled = false;
        }

        //connections = new Element.ConnectionTypes[4];
        //part.element.connections.CopyTo(connections, 0);
        //connections = element.connections;
        
        if(isFixed)
        {
            //ElementTurn(part.fixTurns, true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isFixed)
        {
            ElementTurn();
        }
        else
        {
            ShakeAnimation();
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
                transform.localRotation = Quaternion.Euler(0, 0, targetAngle);
                targetAngle = transform.localRotation.eulerAngles.z;
            }
        }
    }

    private IEnumerator CoroutineShake()
    {
        transform.localRotation = Quaternion.Euler(0, 0, targetAngle);
        float startAngle = targetAngle;

        for (int i = 0; i <= 5; i++)
        {
            float[] gaps = new float[6] { -14, 10, -7, 5, -4, 0 };
            float shakeTarget = startAngle + gaps[i];

            while (Mathf.Round(transform.localRotation.eulerAngles.z) != Mathf.Round(shakeTarget))
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, shakeTarget), .55f);
                yield return null;
            }
        }

        yield break;
    }

    private void ShakeAnimation()
    {
        if (coroutineShake != null)
        {
            StopCoroutine(coroutineShake);
        }

        coroutineShake = StartCoroutine(CoroutineShake());
    }

    private void ConnectionsShift()
    {
        Element.ConnectionTypes tempConnection = connections[3];
        
        for(int i = 3; i > 0; i--)
        {
            connections[i] = connections[i - 1];
        }

        connections[0] = tempConnection;
    }
}
