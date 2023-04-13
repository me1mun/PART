using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InfiniteScrollCase : MonoBehaviour
{
    [SerializeField] private int index;

    public UnityEvent OnInit;

    public int GetIndex()
    {
        return index;
    }

    public void Init(int newIndex)
    {
        index = newIndex;
        OnInit.Invoke();
    }
}
