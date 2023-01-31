using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Element", menuName = "Elements", order = 1)]
public class Element : ScriptableObject
{
    public Sprite icon;

    public enum ConnectionTypes {none, regular, universal};
    public ConnectionTypes[] connections = new ConnectionTypes[4];
}
