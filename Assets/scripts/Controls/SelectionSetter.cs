using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSetter : MonoBehaviour
{
    public GlobalSelect selectionType;

    public void SetSingle()
    {
        selectionType.typeOfSelect = TypeOfSelect.single;
    }

    public void SetColumn()
    {
        selectionType.typeOfSelect = TypeOfSelect.column;
    }
    public void SetLayer()
    {
        selectionType.typeOfSelect = TypeOfSelect.layer;
    }
}
