using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GlobalSelect : ScriptableObject
{
    public TypeOfSelect typeOfSelect;
}

public enum TypeOfSelect
{
    single,
    column,
    layer
}
