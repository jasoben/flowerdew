using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class CubeBuffer : MonoBehaviour {

    //Colors
    private static List<Color> cubeColors;
    private static Color blue;
    private static Color green;

    //GameObjects
    private static GameObject[][] cubeLayers;
    private static List<GameObject> allCubes;
    private static List<GameObject> selectedCubes;
    public static List<GameObject> SelectedCubes
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
    private static int matrixDepth;
    private static int matrixXSize;
    private static int matrixZSize;
    private static int currentCount;

    //Text
    private static string cubeNameWithoutDepth;

    private void Awake()
    {
                
        //Event Handling
        MasterScript.ObjectsDone += RunWhenObjectDataLoaded;

    }
    private void Start()
    {
        //Colors
        cubeColors = new List<Color>();
        blue = new Color(0, .6f, 1);
        green = new Color(0, 1, 0);
        
        //GameObjects
        allCubes = new List<GameObject>();
        selectedCubes = new List<GameObject>();
        allCubes = MasterScript.AllCubes;
        
    }

    private void Update()
    {
        
    }

    private void RunWhenObjectDataLoaded()
    {
        //Numbers
        matrixDepth = MasterScript.MatrixDepth;
        matrixXSize = MasterScript.MatrixXSize;
        matrixZSize = MasterScript.MatrixZSize;
        currentCount = 0;

        //GameObjects
        cubeLayers = new GameObject[matrixDepth][];

        for (int i = 0; i < matrixDepth; i++)
        {
            cubeLayers[i] = GameObject.FindGameObjectsWithTag("level" + i.ToString()); //These go into an array for easy selection; even though it would be possible to search the list each time, it's faster to define the array early for whole-layer selection later
        }
        
        MasterScript.ObjectsDone -= RunWhenObjectDataLoaded;
    }
    public static void SelectSingleCube(GameObject thisSelectedCube)
    {
        selectedCubes.Add(thisSelectedCube);
        HighLightCubes(green);
    }

    public static void SelectColumn(GameObject thisSelectedCube)
    {
        for (int d = 0; d < matrixDepth; d++)
        {

            cubeNameWithoutDepth = thisSelectedCube.name.Remove(thisSelectedCube.name.Length - 1, 1);
            
            foreach (GameObject cubeWithSimilarName in allCubes)
            {
                if (cubeWithSimilarName.name.Contains(cubeNameWithoutDepth))
                {
                    selectedCubes.Add(cubeWithSimilarName);
                    cubeWithSimilarName.SetActive(true);
                    cubeWithSimilarName.transform.Find("cubeLetter").transform.gameObject.SetActive(true);
                    cubeWithSimilarName.transform.Find("squareNumber").transform.gameObject.SetActive(true);
                    cubeWithSimilarName.transform.Find("layerNumber").transform.gameObject.SetActive(true);
                    HighLightCubes(blue);
                    
                }

            }
        }
    }

    public static void SelectLayer(GameObject thisSelectedCube)
    {
        string levelTagOfThisCube = thisSelectedCube.tag.Replace("level", "");
        int levelIntegerOfThisCube = Int32.Parse(levelTagOfThisCube); //Parse the name of the cube to return a value so we can select based on an array of cube layers- this is faster than searching the list each time
        
        foreach (GameObject theCubeInThisLayer in cubeLayers[levelIntegerOfThisCube])
        {
            selectedCubes.Add(theCubeInThisLayer);
            if (theCubeInThisLayer.name.Contains("Layer")) selectedCubes.Remove(theCubeInThisLayer); //This removes the layer indicator boxes from the selection
        }
        HighLightCubes(green);
    }
    
    public static void ClearBuffer()
    {
        if (selectedCubes.Count > 0)
        {
            
            for (int i = 0; i < selectedCubes.Count; i++)
            {
                selectedCubes[i].GetComponent<Renderer>().material.color = cubeColors[i];                
            }
            selectedCubes.Clear();
            cubeColors.Clear();
            LayerNavigator.ClearCubesAboveLayer();
            currentCount = 0;
        }
    }

    private static void HighLightCubes(Color color)
    {
        RemoveDuplicateCubes();
        StoreColorValue();
        
        foreach (GameObject thisCube in selectedCubes)
        {
            //this conditional checks to see if it's already been colored
            if (thisCube.GetComponent<Renderer>().material.color.r != 0)
                thisCube.GetComponent<Renderer>().material.color = thisCube.GetComponent<Renderer>().material.color * color;
        }
    }

    private static void StoreColorValue()
    {
        for (int i = currentCount; i < selectedCubes.Count; i++)
        {
            cubeColors.Add(selectedCubes[i].GetComponent<Renderer>().material.color);
        }
        currentCount = selectedCubes.Count;
    }

    private static void RemoveDuplicateCubes()
    {
        selectedCubes = selectedCubes.Distinct().ToList();
    }
    public void ClearButton()
    {
        ClearBuffer();
    }
}

