using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class CubeBuffer : MonoBehaviour {

    //GameObjects
    private GameObject masterObject;

    private List<GameObject> allCubes;
    public List<GameObject> AllCubes
    {
        get
        {
            return allCubes;
        }
    }

    private List<GameObject> selectedCubes;
    public List<GameObject> SelectedCubes
    {
        get
        {
            return selectedCubes;
        }
    }

    //Numbers
    private int matrixDepth;

    //Text
    private string cubeNameWithoutDepth;

    private void Start()
    {
        //GameObjects
        masterObject = GameObject.Find("MasterObject");

        //Numbers
        matrixDepth = masterObject.GetComponent<MasterScript>().MatrixDepth;
    }

    private void Update()
    {
        
    }

    public void SelectColumn(GameObject thisSelectedCube)
    {
        for (int d = 0; d < matrixDepth; d++)
        {

            cubeNameWithoutDepth = thisSelectedCube.name.Remove(this.name.Length - 1, 1);

            foreach (GameObject cubeWithSimilarName in allCubes)
            {

                if (cubeWithSimilarName.name.Contains(cubeNameWithoutDepth + d.ToString()))
                {
                    selectedCubes.Add(this.transform.gameObject);
                }

            }
        }
    }

    public void SelectLayer(GameObject thisSelectedCube)
    {
        string levelTagOfThisCube = thisSelectedCube.tag.Replace("level", "");
        int levelIntegerOfThisCube = Int32.Parse(levelTagOfThisCube);

        foreach (GameObject theCubeInThisLayer in matrix.cubeLayers[levelIntegerOfThisCube])
        {
            theseCubes.Add(theCubeInThisLayer);
            if (theCubeInThisLayer.name.Contains("Layer")) theseCubes.Remove(theCubeInThisLayer);
        }
    }
}
