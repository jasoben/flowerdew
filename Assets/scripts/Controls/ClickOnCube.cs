using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour
{
    Controls navigationControls;
    public GlobalSelect selectType;

    private GameObject cubeLetter, level;

    private bool cubeSelected;

    public bool CubeSelected
    {
        get { return cubeSelected; }
        set { cubeSelected = value; }
    }

    private void Awake()
    {
        cubeSelected = false;
    }
    // Use this for initialization
    void Start()
    {
        navigationControls = GameObject.Find("UserViewpoint").GetComponent<Controls>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseOver()
    {
        if (!cubeSelected)
        {
            GetComponent<Renderer>().material.color = MasterScript.HighlightColor;
        }
    }
    void OnMouseExit()
    {
        if (!cubeSelected)
        {
            GetComponent<Renderer>().material.color = MasterScript.DefaultCubeColor;
        }
    }
     void OnMouseDown()
    {
        switch (navigationControls.thisClick)
        {
            case TypeOfClick.select:
            {
                //This is the conditional for selecting columns

                if (Input.GetKey(KeyCode.LeftControl) || selectType.typeOfSelect == TypeOfSelect.column)
                {
                    CubeBuffer.SelectColumn(this.transform.gameObject);
                }

                //This is the conditional for selecting the whole layer

                else if (Input.GetKey(KeyCode.LeftShift)|| selectType.typeOfSelect == TypeOfSelect.layer)
                {
                    CubeBuffer.SelectLayer(this.transform.gameObject);
                }

                //This is for the condition when SHIFT is not held down, i.e. to select a single cube instead of a column
                else
                {
                    CubeBuffer.SelectSingleCube(this.transform.gameObject);
                }

                CubeSelected = true;
                MasterScript.CubeClickedWhileBigCubeHidden = true;
                break;
            }
            case TypeOfClick.move:
            {
                break;

            }
            case TypeOfClick.view:
            {
                break;
            }

               

        }
    }

}
