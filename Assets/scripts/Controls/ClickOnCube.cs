using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour
{
    Controls navigationControls;

    private GameObject cubeLetter, level;

    private bool cubeSelected;

    public Color selectedCubeColor;
    public Color defaultCubeColor;

    public bool CubeSelected
    {
        get { return cubeSelected; }
        set { cubeSelected = value; }
    }

    // Use this for initialization
    void Start()
    {
        navigationControls = GameObject.Find("UserViewpoint").GetComponent<Controls>();
        cubeSelected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseOver()
    {
        GetComponent<Renderer>().material.color = selectedCubeColor;
    }
    void OnMouseExit()
    {
        if (!cubeSelected)
        {
            GetComponent<Renderer>().material.color = defaultCubeColor;
        }
    }
     void OnMouseDown()
    {
        switch (navigationControls.thisClick)
        {
            case TypeOfClick.select:
            {
                //This is the conditional for selecting columns

                if (Input.GetKey(KeyCode.C))
                {
                    CubeBuffer.SelectColumn(this.transform.gameObject);
                }

                //This is the conditional for selecting the whole layer

                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    CubeBuffer.SelectLayer(this.transform.gameObject);
                }

                //This is for the condition when SHIFT is not held down, i.e. to select a single cube instead of a column
                else
                {
                    CubeBuffer.SelectSingleCube(this.transform.gameObject);
                }
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
