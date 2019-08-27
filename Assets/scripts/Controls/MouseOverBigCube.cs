using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverBigCube : MonoBehaviour
{
    
    private bool cubeEnabled;
    public GameObject block, layer;
    private Renderer bigCubeRenderer;
    public IntList selectedBlocks;
    private string thisBlockNumber;
    private int thisBlockNumberInt;

    // Use this for initialization
    void Start()
    {
        cubeEnabled = true;
        bigCubeRenderer = GetComponent<Renderer>();
        thisBlockNumber = block.GetComponent<TextMesh>().text;
        thisBlockNumberInt = int.Parse(thisBlockNumber);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (int thisBlock in selectedBlocks.ints)
        {
            if (thisBlock == thisBlockNumberInt && cubeEnabled)
            {
                DisableBigCube();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SelectorRay" && cubeEnabled)
        {
            DisableBigCube();
        }
   }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SelectorRay" && !cubeEnabled)
        {
            EnableBigCube();
        }
    }

    public void DisableBigCube()
    {
        bigCubeRenderer.enabled = false;
        block.SetActive(false);
        layer.SetActive(false);
        cubeEnabled = false;
    }

    public void EnableBigCube()
    {
        bigCubeRenderer.enabled = true;
        block.SetActive(true);
        layer.SetActive(true);
        cubeEnabled = true;
    }
}
