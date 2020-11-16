using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeomeryData", menuName = "ScriptableObjects/GeometryObjectData", order = 1)]
public class ClickColorData : ScriptableObject
{
    public string ObjectType;
    public int MinClicksCount;
    public int MaxClicksCount;
    public Color Color;
}
