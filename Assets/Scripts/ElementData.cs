using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementData : MonoBehaviour
{
    public static ElementData Instance { get; private set; }

    public Element[] elements;
    public Dictionary<string, Element> elementList = new Dictionary<string, Element>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        FillElementList();
    }

    private void FillElementList()
    {
        foreach (Element el in elements)
        {
            elementList.Add(el.name, el);
        }
    }
}
