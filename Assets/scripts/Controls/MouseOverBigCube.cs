using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverBigCube : MonoBehaviour
{
    
    private bool cubeSelected, sentDown;
    public GameObject block, layer;
    private Vector3 originalPosition;

    // Use this for initialization
    void Start()
    {
        cubeSelected = false;
        sentDown = false;
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SelectorRay")
        {
            if (!sentDown)
            {
                originalPosition = gameObject.transform.position;
                gameObject.transform.Translate(0, -.5f, 0);
                sentDown = true;
            }
        }
   }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SelectorRay")
        {
            Debug.Log("Exit");
            if (sentDown)
            {
                gameObject.transform.position = originalPosition;
                sentDown = false;
            }
        }
    }
}
