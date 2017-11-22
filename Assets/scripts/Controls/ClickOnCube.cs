using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour
{
       

    // Use this for initialization
    void Start()
    {
                
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
        
    }

}
