using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CubeBuffer : MonoBehaviour {

    public delegate void CubeLayersCreated();
    public static CubeLayersCreated CubesAreCreated;
    public UnityEvent clearSelectedCubes;
    public IntList selectedBlocks;

    //Colors
    private static Color cubeColor;
    private static Color selectedColor;

    //GameObjects
    private static GameObject[][] cubeLayers;
    private static GameObject[] cubeLayerHoldingEmptyObjects;
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
        cubeColor = MasterScript.DefaultCubeColor;
        selectedColor = MasterScript.SelectedCubeColor;
        
        //GameObjects
        allCubes = new List<GameObject>();
        selectedCubes = new List<GameObject>();
        allCubes = MasterScript.AllCubes;
        print("test");

        Invoke("ResetEverything", .5f);

    }

    private void ResetEverything()
    {
        clearSelectedCubes.Invoke();
    }
    public void ClearSelectionList()
    {
        selectedBlocks.ints.Clear();
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
        cubeLayerHoldingEmptyObjects = new GameObject[matrixDepth];

        for (int i = 0; i < matrixDepth; i++)
        {
            //These go into an array for easy selection; even though it would be possible to 
            //search the list each time, it's faster to define the array early for whole-layer selection later
            cubeLayers[i] = GameObject.FindGameObjectsWithTag("level" + i.ToString());
            cubeLayerHoldingEmptyObjects[i] = GameObject.Find("Layer " + i.ToString());
        }
        CubesAreCreated();
        MasterScript.ObjectsDone -= RunWhenObjectDataLoaded;
    }
    public static void SelectSingleCube(GameObject thisSelectedCube)
    {
        selectedCubes.Add(thisSelectedCube);
        HighLightCubes(selectedColor);
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
                    //cubeWithSimilarName.transform.Find("layerNumber").transform.gameObject.SetActive(true);
                    HighLightCubes(selectedColor);
                    
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
            if (theCubeInThisLayer.name.Contains("BigCube")) selectedCubes.Remove(theCubeInThisLayer); //This removes the layer indicator boxes from the selection
        }
        HighLightCubes(selectedColor);
    }
    
    public static void ClearBuffer()
    {
        if (selectedCubes.Count > 0)
        {
            for (int i = 0; i < selectedCubes.Count; i++)
            {
                selectedCubes[i].GetComponent<Renderer>().material.color = cubeColor;
                selectedCubes[i].GetComponent<ClickOnCube>().CubeSelected = false;
            }
            selectedCubes.Clear(); //clear the small cubes
            foreach (GameObject thisBigCube in MasterScript.AllBigCubes) //clear the big cubes
            {
                thisBigCube.SetActive(true);
            }
            MasterScript.LastSelectedBigCube.Clear();
            LayerNavigator.ClearCubesAboveLayer();
            currentCount = 0;

            foreach (GameObject layerHolder in cubeLayerHoldingEmptyObjects)
            {
                layerHolder.GetComponent<CubeTextEnabler>().HideText();
            }
        }
    }
    
    public static void TextVisibilitySwitcher()
    {
        foreach (GameObject layerHolder in cubeLayerHoldingEmptyObjects)
        {
            layerHolder.GetComponent<CubeTextEnabler>().ShowOrHideText();
        }
    }
    public static void HideAllCubesExceptSelected()
    {
        foreach (GameObject thisCube in MasterScript.AllCubes)
        {
            thisCube.SetActive(false);
        }
        foreach (GameObject thisBigCube in MasterScript.AllBigCubes)
        {
            thisBigCube.SetActive(false);
        }
        foreach (GameObject thisSelectedCube in selectedCubes)
        {
            thisSelectedCube.SetActive(true);
        }
    }
    public static void HighLightCubes(Color color)
    {
        RemoveDuplicateCubes();
        
        foreach (GameObject thisCube in selectedCubes)
        {
            thisCube.GetComponent<ClickOnCube>().CubeSelected = true;
            thisCube.GetComponent<Renderer>().material.color = color;
        }

        foreach (GameObject thisObject in selectedCubes)
        {
            thisObject.transform.Find("cubeLetter").transform.gameObject.SetActive(true);
            thisObject.transform.Find("squareNumber").transform.gameObject.SetActive(true);
            thisObject.transform.Find("layerNumber").transform.gameObject.SetActive(true);
        }
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

