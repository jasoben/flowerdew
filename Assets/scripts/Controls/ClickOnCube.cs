using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ClickOnCube : MonoBehaviour
{
    Controls navigationControls;
    public GlobalSelect selectType;
    public IntList selectedBlocks;
    private int blockNumber;
    public GameObject block;
    public UnityEvent clearAllBigCubes;

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
        blockNumber = int.Parse(block.GetComponent<TextMesh>().text); 
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
                    if (!selectedBlocks.ints.Contains(blockNumber))
                    {
                        selectedBlocks.ints.Add(blockNumber);
                    }

                    if (Input.GetKey(KeyCode.LeftControl) || selectType.typeOfSelect == TypeOfSelect.column)
                    {
                        CubeBuffer.SelectColumn(this.transform.gameObject);
                    }

                    //This is the conditional for selecting the whole layer

                    else if (Input.GetKey(KeyCode.LeftShift)|| selectType.typeOfSelect == TypeOfSelect.layer)
                    {
                        clearAllBigCubes.Invoke();
                        selectedBlocks.ints.Clear();
                        selectedBlocks.ints.Add(177);
                        selectedBlocks.ints.Add(178);
                        selectedBlocks.ints.Add(179);
                        selectedBlocks.ints.Add(180);
                        selectedBlocks.ints.Add(247);
                        selectedBlocks.ints.Add(248);
                        selectedBlocks.ints.Add(249);
                        selectedBlocks.ints.Add(316);
                        selectedBlocks.ints.Add(317);
                        selectedBlocks.ints.Add(318);
                        selectedBlocks.ints.Add(319);
                        selectedBlocks.ints.Add(386);
                        selectedBlocks.ints.Add(387);
                        selectedBlocks.ints.Add(388);
                        selectedBlocks.ints.Add(389);

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
