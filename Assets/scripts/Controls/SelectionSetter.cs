using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSetter : MonoBehaviour
{
    public GlobalControl controls;
    public Button single, column, layer,
        select, view, move; 

    public void SetSingle()
    {
        controls.typeOfSelect = TypeOfSelect.single;
        ShowAllSelectButtons();
        single.interactable = false;
    }

    public void SetColumn()
    {
        controls.typeOfSelect = TypeOfSelect.column;
        ShowAllSelectButtons();
        column.interactable = false;
    }
    public void SetLayer()
    {
        controls.typeOfSelect = TypeOfSelect.layer;
        ShowAllSelectButtons();
        layer.interactable = false;
    }
    public void SetSelect()
    {
        controls.typeOfClick = TypeOfClick.select;
        ShowAllClickButtons();
        select.interactable = false;
    }
    public void SetView()
    {
        controls.typeOfClick = TypeOfClick.view;
        ShowAllClickButtons();
        view.interactable = false;
    }
    public void SetMove()
    {
        controls.typeOfClick = TypeOfClick.move;
        ShowAllClickButtons();
        move.interactable = false;
    }


    private void ShowAllSelectButtons()
    {
        single.interactable = true;
        column.interactable = true;
        layer.interactable = true;
    }
    private void ShowAllClickButtons()
    {
        select.interactable = true;
        view.interactable = true;
        move.interactable = true;
    }
}
