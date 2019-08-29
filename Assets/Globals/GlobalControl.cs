using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GlobalControl : ScriptableObject
{
    public TypeOfClick typeOfClick;
    public TypeOfSelect typeOfSelect;
}

public enum TypeOfSelect
{
    single,
    column,
    layer
}
