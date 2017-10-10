using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour
{
    //Custom Classes
    private CubeBuffer cubeBuffer;

    //GameObjects
    private GameObject masterObject;
    
    //Text
    private string myNameIs;
    public string MyNameIs { get { return myNameIs; } set { } }
    public Text cubeInfo;
    private string cubeNameWithoutDepth;

    // Use this for initialization
    void Start()
    {
        //GameObjects    
        masterObject = GameObject.Find("MasterObject");
        cubeBuffer = GameObject.Find("MasterObject").GetComponent<CubeBuffer>();
                
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {

        
        //This is the conditional for selecting columns

        if (Input.GetKey(KeyCode.C))
        {
            cubeBuffer.SelectColumn(this.transform.gameObject);
        }

        //This is the conditional for selecting the whole layer

        else if (Input.GetKey(KeyCode.LeftShift))
        {

            



        }
        //This is for the condition when SHIFT is not held down, i.e. to select a single cube instead of a column

        else
        {
            transparentOrNot = false;
            showAllCubes = false;

            //cubeInfo.text = myNameIs;
            theseCubes.Add(this.gameObject);





        }

        

        //TODO fix problem where selecting whole layer, then selecting same individual cube, leaves whole layer selected
    }



}
