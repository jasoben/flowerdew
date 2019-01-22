using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverBigCube : MonoBehaviour
{

    private bool cubeSelected;

    // Use this for initialization
    void Start()
    {
        cubeSelected = false;
        MasterScript.LastSelectedBigCube = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseOver()
    {

        //deactivate this cube and re-activate the one stored in the buffer, then store this one in the buffer
        gameObject.SetActive(false);
        MasterScript.LastSelectedBigCube.SetActive(true);
        Debug.Log(MasterScript.LastSelectedBigCube);
        MasterScript.LastSelectedBigCube = gameObject;
        Debug.Log(MasterScript.LastSelectedBigCube);

    }
}
