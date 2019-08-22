using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSetter : MonoBehaviour
{
    public GlobalSelect selectionType;
    public Button single, column, layer; 

    public void SetSingle()
    {
        selectionType.typeOfSelect = TypeOfSelect.single;
        ShowAllButtons();
        single.interactable = false;
    }

    public void SetColumn()
    {
        selectionType.typeOfSelect = TypeOfSelect.column;
        ShowAllButtons();
        column.interactable = false;
    }
    public void SetLayer()
    {
        selectionType.typeOfSelect = TypeOfSelect.layer;
        ShowAllButtons();
        layer.interactable = false;
    }

    private void ShowAllButtons()
    {
        single.interactable = true;
        column.interactable = true;
        layer.interactable = true;

    }
}
