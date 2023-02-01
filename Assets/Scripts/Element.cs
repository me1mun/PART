using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Element", menuName = "Elements", order = 1)]
public class Element : ScriptableObject
{
    public Sprite icon;

    public bool isFixed = false;

    public enum ConnectionTypes {none, regular, universal};
    public ConnectionTypes connectionLeft= ConnectionTypes.none;
    public ConnectionTypes connectionUp = ConnectionTypes.none;
    public ConnectionTypes connectionRight = ConnectionTypes.none;
    public ConnectionTypes connectionDown = ConnectionTypes.none;
}
