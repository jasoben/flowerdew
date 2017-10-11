using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class CubeBuffer : MonoBehaviour {

    //GameObjects
    private GameObject masterObject;
    private GameObject[][] cubeLayers;
    private List<GameObject> allCubes;
    private List<GameObject> selectedCubes;
    public List<GameObject> SelectedCubes
    {
        get
        {
            return selectedCubes;
        }
    }
    private List<GameObject> cubeText;
    public List<GameObject> CubeText
    {
        get { return cubeText; }
    }

    //Numbers
    private int matrixDepth;
    private int matrixXSize;
    private int matrixZSize;

    //Text
    private string cubeNameWithoutDepth;

    private void Start()
    {
        //EARLY
        masterObject = GameObject.Find("MasterObject");
        matrixDepth = masterObject.GetComponent<MasterScript>().MatrixDepth;
        matrixXSize = masterObject.GetComponent<MasterScript>().MatrixXSize;
        matrixZSize = masterObject.GetComponent<MasterScript>().MatrixZSize;

        //GameObjects
        //masterObject = set in EARLY
        allCubes = new List<GameObject>();
        selectedCubes = new List<GameObject>();
        allCubes = masterObject.GetComponent<MasterScript>().AllCubes;
        cubeLayers = new GameObject[matrixDepth][];
        for (int i = 0; i < matrixDepth; i++)
        {
            cubeLayers[i] = GameObject.FindGameObjectsWithTag("level"+i.ToString()); //These go into an array for easy selection; even though it would be possible to search the list each time, it's faster to define the array early for whole-layer selection later
        }
        //foreach (GameObject cube in allCubes)
        //{
        //    cubeText.Add(cube.transform.Find("cubeLetter").gameObject);
        //    cubeText.Add(cube.transform.Find("squareNumber").gameObject);
        //}

        //TODO fix the above method

        //Numbers
        //matrixDepth  
        //matrixXSize  
        //matrixZSize = defined in EARLY


    }

    private void Update()
    {
        
    }

    public void SelectSingleCube(GameObject thisSelectedCube)
    {
        selectedCubes.Add(thisSelectedCube);
    }

    public void SelectColumn(GameObject thisSelectedCube)
    {
        for (int d = 0; d < matrixDepth; d++)
        {

            cubeNameWithoutDepth = thisSelectedCube.name.Remove(thisSelectedCube.name.Length - 1, 1);

            foreach (GameObject cubeWithSimilarName in allCubes)
            {

                if (cubeWithSimilarName.name.Contains(cubeNameWithoutDepth + d.ToString()))
                {
                    selectedCubes.Add(cubeWithSimilarName);
                }

            }
        }
    }

    public void SelectLayer(GameObject thisSelectedCube)
    {
        string levelTagOfThisCube = thisSelectedCube.tag.Replace("level", "");
        int levelIntegerOfThisCube = Int32.Parse(levelTagOfThisCube); //Parse the name of the cube to return a value so we can select based on an array of cube layers- this is faster than searching the list each time

        foreach (GameObject theCubeInThisLayer in cubeLayers[levelIntegerOfThisCube])
        {
            selectedCubes.Add(theCubeInThisLayer);
            if (theCubeInThisLayer.name.Contains("Layer")) selectedCubes.Remove(theCubeInThisLayer); //This removes the layer indicator boxes from the selection
        }
    }
}
