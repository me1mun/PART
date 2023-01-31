using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] Animation anim;

    void Start()
    {
        isOpen = false;
        anim["Menu"].speed = -1;
        anim["Menu"].time = 0;
        anim.Play();
    }

    private void Update()
    {

    }

    public void Activator(bool activate)
    {
        isOpen = activate;

        if (isOpen)
        {
            anim["Menu"].speed = 1;
        }
        else
        {
            anim["Menu"].speed = -1;
            anim["Menu"].time = anim["Menu"].length;
        }

        anim.Play();
    }
}
