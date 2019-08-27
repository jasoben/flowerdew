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
        Debug.Log("Triggered!");
        if (!sentDown)
        {
            originalPosition = gameObject.transform.position;
            gameObject.transform.Translate(0, -20, 0);
            sentDown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("UnTriggered!");
        if (sentDown)
        {
            gameObject.transform.position = originalPosition;
            sentDown = false;
        }
    }
}
