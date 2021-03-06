﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class CreateMatrix : MonoBehaviour
{

    public int matrixWidth;
    public int matrixHeight;
    public int matrixDepth;

    private bool nowCubesAreTransparent;
    private bool noMoreTransparencyNeeded;
    private bool textIsToggledOff;
    private bool allCubesActive;

    private float layerDepthColorGradientSpread;
    private float layerDepthColorStartingValue;

    public GameObject matrixCube;
    public GameObject matrixCenter;
    private GameObject renderSmartSquare;
    private GameObject[] renderSmartSquareArray;
    private List<GameObject>[] blocksToDisableToDecreaseMemoryLoad;

    public Material groundColor;
    private Color newBlockColor;
    private Color[] thisLayersColor;
    public Material passiveColor;
    public Material activeColor;
    public Material transparentColor;

    private int matrixSize;

    public Vector3 matrixOrigin;

    private float matrixCubeWidth;
    private float matrixCubeHeight;
    private float matrixCubeDepth;

    private float interCubeDistance = 1.05f;
    private float interBigSquareDistance = 5.2f;

    public GameObject[] cubesInMatrix;
    public GameObject[][] cubeLayers;

    public List<GameObject> cubeText;

    public List<GameObject> totalCubesInMatrix;

    public GameObject currentCube;
    public GameObject layerIndicatorBlock;
    private GameObject currentLayerIndicatorBlock;

    public string moveUpLayer;
    public string moveDownLayer;

    private int currentLayer;

    private GameObject[] currentCubeColumn;

    public GameObject[] CurrentCubeColumn
    {
        get
        {
            return currentCubeColumn;
        }

        set
        {

        }
    }

    private List<GameObject> selectedCubes;

    public List<GameObject> SelectedCubes
    {
        get
        {
            return selectedCubes;
        }

        set
        {

        }
    }

    // Use this for initialization
    void Start()
    {

        matrixCenter = this.gameObject;

        matrixCubeHeight = matrixCubeWidth = 1.15f;
        matrixCubeDepth = .5f;

        matrixWidth = 6;
        matrixHeight = 4;
        matrixDepth = 8;

        nowCubesAreTransparent = false;
        noMoreTransparencyNeeded = false;
        textIsToggledOff = false;
        allCubesActive = false;

        layerDepthColorGradientSpread = .05f;
        layerDepthColorStartingValue = .5f;
        thisLayersColor = new Color[matrixDepth];

        matrixSize = matrixWidth * matrixHeight * matrixDepth;

        cubesInMatrix = new GameObject[matrixSize];
        totalCubesInMatrix = new List<GameObject>();
        selectedCubes = new List<GameObject>();

        cubeLayers = new GameObject[matrixDepth][];
        renderSmartSquareArray = new GameObject[matrixDepth];
        blocksToDisableToDecreaseMemoryLoad = new List<GameObject>[matrixDepth];

        for (int i = 0; i < blocksToDisableToDecreaseMemoryLoad.Length; i++)
        {
            blocksToDisableToDecreaseMemoryLoad[i] = new List<GameObject>();
        }

        matrixOrigin = new Vector3(FixNumber(matrixWidth), FixNumber(matrixHeight), FixNumber(matrixDepth));

        matrixCenter.transform.position = matrixOrigin;

        currentCubeColumn = new GameObject[matrixDepth];

        currentLayer = matrixDepth - 1;

        for (int d = 0; d < matrixDepth; d++)
        {

            float e = d; // This is used to change the color value of the layer blocks
            float f = d / 2;
            if (d > 6)
            {
                newBlockColor = new Color(layerDepthColorStartingValue * 1f * f, layerDepthColorStartingValue * .7f * f, layerDepthColorStartingValue * .7f * f);
            }
            else if (d < 6)
            {
                newBlockColor = new Color(layerDepthColorStartingValue * .3f * e, layerDepthColorStartingValue * .3f * e, layerDepthColorStartingValue * .5f * e);
            }

            //GameObject[] largeSquare387 = CreateLargeSquare(387, 0, 0, d);
            //GameObject[] largeSquare388 = CreateLargeSquare(388, 1, 0, d);
            //GameObject[] largeSquare389 = CreateLargeSquare(389, 2, 0, d);

            //GameObject[] largeSquare317 = CreateLargeSquare(317, 0, 1, d);
            //GameObject[] largeSquare318 = CreateLargeSquare(318, 1, 1, d);
            //GameObject[] largeSquare319 = CreateLargeSquare(319, 2, 1, d);

            //GameObject[] largeSquare247 = CreateLargeSquare(247, 0, 2, d);
            //GameObject[] largeSquare248 = CreateLargeSquare(248, 1, 2, d);
            //GameObject[] largeSquare249 = CreateLargeSquare(249, 2, 2, d);

            //GameObject[] largeSquare177 = CreateLargeSquare(177, 0, 3, d);
            //GameObject[] largeSquare178 = CreateLargeSquare(178, 1, 3, d);
            //GameObject[] largeSquare179 = CreateLargeSquare(179, 2, 3, d);


            currentLayerIndicatorBlock = Instantiate(layerIndicatorBlock, new Vector3(11, -d * matrixCubeDepth * interCubeDistance, 22), Quaternion.identity);
            currentLayerIndicatorBlock.GetComponent<Renderer>().material.color = newBlockColor;
            thisLayersColor[d] = newBlockColor;
            currentLayerIndicatorBlock.transform.Find("LayerIndicatorText").GetComponent<TextMesh>().text = "Level " + d;
            currentLayerIndicatorBlock.tag = "level" + d;

            //create some large squares to stand in for layers when they're not visible (to increase performance by rendering fewer independent meshes)

            renderSmartSquare = Instantiate(matrixCube, new Vector3(8.9f, d * matrixCubeDepth * interCubeDistance, 7.9f), Quaternion.identity);
            renderSmartSquare.GetComponent<MeshRenderer>().material = transparentColor;
            renderSmartSquare.transform.localScale = new Vector3(16.4f, .5f, 13.5f);
            DeActivateTextOnCube(renderSmartSquare);
            renderSmartSquare.name = "renderSmartSquare" + d;
            renderSmartSquare.tag = "level" + d;
            Destroy(renderSmartSquare.GetComponent<ClickOnCube>());
            renderSmartSquare.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
            renderSmartSquareArray[d] = renderSmartSquare;

        }

        foreach (GameObject specificCube in cubesInMatrix)
        {
            cubeLayers[0] = GameObject.FindGameObjectsWithTag("level0");
            cubeLayers[1] = GameObject.FindGameObjectsWithTag("level1");
            cubeLayers[2] = GameObject.FindGameObjectsWithTag("level2");
            cubeLayers[3] = GameObject.FindGameObjectsWithTag("level3");
            cubeLayers[4] = GameObject.FindGameObjectsWithTag("level4");
            cubeLayers[5] = GameObject.FindGameObjectsWithTag("level5");
            cubeLayers[6] = GameObject.FindGameObjectsWithTag("level6");
            cubeLayers[7] = GameObject.FindGameObjectsWithTag("level7");
        }

        foreach (GameObject cube in totalCubesInMatrix)
        {

            cubeText.Add(cube.transform.Find("cubeLetter").gameObject);
            cubeText.Add(cube.transform.Find("squareNumber").gameObject);

        }

        //Assign cubes that intersect renderSmartSquares to arrays so they can be turned on and off when needed (this saves on processing)

        foreach (GameObject cube in totalCubesInMatrix)
        {
            for (int i = 0; i < renderSmartSquareArray.Length; i++)
            {
                if (cube.GetComponent<BoxCollider>().bounds.Intersects(renderSmartSquareArray[i].GetComponent<BoxCollider>().bounds))
                {
                    blocksToDisableToDecreaseMemoryLoad[i].Add(cube);
                }
            }

        }

        for (int i = 0; i < currentLayer; i++)
        {
            foreach (GameObject cube in blocksToDisableToDecreaseMemoryLoad[i])
            {
                cube.SetActive(false);
            }
        }

        renderSmartSquareArray[currentLayer].SetActive(false);




        // This Coroutine does fancy box stacking at the start of the program, just for visual effect, nothing more

        //  StartCoroutine("StackBoxes");

    }

    // Update is called once per frame
    void Update()
    {


        // Controls for viewing the layers

        if (Input.GetKeyDown(moveDownLayer) == true)
        {
            for (int i = 0; i < cubeLayers[currentLayer].Length; i++)
            {
                cubeLayers[currentLayer][i].SetActive(false);
            }



            if (allCubesActive == false) HideSquares();

            currentLayer--;

            if (currentLayer < 0)
            {
                currentLayer = -1;

                //TODO figure out why I need this to be -1 because it's causing an array error
            }
        }

        else if (Input.GetKeyDown(moveUpLayer) == true)
        {

            if (currentLayer > 0)
            {
                for (int i = 0; i < cubeLayers[currentLayer].Length; i++)
                {
                    cubeLayers[currentLayer][i].SetActive(true);
                }
            }

            currentLayer++;

            if (currentLayer > matrixDepth - 1)
            {
                currentLayer = matrixDepth - 1;
            }

            for (int i = 0; i < cubeLayers[currentLayer].Length; i++)
            {
                cubeLayers[currentLayer][i].SetActive(true);
            }

            if (allCubesActive == false) ShowSquares();
            else if (allCubesActive == true)
            {
                renderSmartSquareArray[currentLayer].SetActive(false);
                renderSmartSquareArray[currentLayer - 1].SetActive(false);
            }
        }



    }

    

    // This method helps calculate the origin of the total grid by running the code through a simple equation

    public float FixNumber(int matrixDimension)
    {
        float newMatrixDimension = (matrixDimension - 1) * interCubeDistance / 2;
        return newMatrixDimension;
    }


    private string ChangeNumberToLetter(string thisName)
    {
        thisName = thisName.Replace("alpha18", "A");
        thisName = thisName.Replace("alpha19", "B");
        thisName = thisName.Replace("alpha20", "C");
        thisName = thisName.Replace("alpha21", "D");
        thisName = thisName.Replace("alpha22", "E");
        thisName = thisName.Replace("alpha23", "F");

        thisName = thisName.Replace("alpha12", "G");
        thisName = thisName.Replace("alpha13", "H");
        thisName = thisName.Replace("alpha14", "J");
        thisName = thisName.Replace("alpha15", "K");
        thisName = thisName.Replace("alpha16", "L");
        thisName = thisName.Replace("alpha17", "M");

        thisName = thisName.Replace("alpha6", "N");
        thisName = thisName.Replace("alpha7", "P");
        thisName = thisName.Replace("alpha8", "Q");
        thisName = thisName.Replace("alpha9", "R");
        thisName = thisName.Replace("alpha10", "S");
        thisName = thisName.Replace("alpha11", "T");

        thisName = thisName.Replace("alpha0", "U");
        thisName = thisName.Replace("alpha1", "V");
        thisName = thisName.Replace("alpha2", "W");
        thisName = thisName.Replace("alpha3", "X");
        thisName = thisName.Replace("alpha4", "Y");
        thisName = thisName.Replace("alpha5", "Z");

        return thisName;
    }

    public void ActivateCubes(List<GameObject> theseCubes, bool transparentOrNot, bool showAllCubes) //This controls how the cubes are activated based on certain conditions
    {

        if (showAllCubes == true && allCubesActive == false)
        {
            for (int i = 0; i < currentLayer; i++)
            {
                renderSmartSquareArray[i].SetActive(false);
            }

            foreach (GameObject cube in totalCubesInMatrix)
            {
                cube.SetActive(true);
            }

            allCubesActive = true;
        }

        else if (showAllCubes == true && allCubesActive == true) { }

        else if (showAllCubes == false && allCubesActive == false) { }

        else if (showAllCubes == false && allCubesActive == true)
        {
            for (int i = 0; i < currentLayer; i++)
            {
                renderSmartSquareArray[i].SetActive(true);
            }

            for (int i = 0; i < currentLayer; i++)
            {
                foreach (GameObject cube in blocksToDisableToDecreaseMemoryLoad[i])
                {
                    cube.SetActive(false);
                }
            }

            allCubesActive = false;
        }


        if (textIsToggledOff && selectedCubes != null)
        {
            foreach (GameObject thisCube in selectedCubes)
            {
                DeActivateTextOnCube(thisCube);
            }
        }

        foreach (GameObject thisCube in selectedCubes)
        {
            string levelTagOfThisCube = thisCube.gameObject.tag.Replace("level", "");
            int levelIntegerOfThisCube = Int32.Parse(levelTagOfThisCube);

            thisCube.GetComponent<Renderer>().material = passiveColor;
            thisCube.GetComponent<Renderer>().material.color = thisLayersColor[levelIntegerOfThisCube];

            Debug.Log(thisCube);
        }

        // Case 1a: to reset the cubes to regular color coming out of transparency mode (e.g. when column is selected)
        if (nowCubesAreTransparent && transparentOrNot == false)
        {
            foreach (GameObject thisCube in totalCubesInMatrix)
            {
                string levelTagOfThisCube = thisCube.gameObject.tag.Replace("level", "");
                int levelIntegerOfThisCube = Int32.Parse(levelTagOfThisCube);

                thisCube.GetComponent<Renderer>().material = passiveColor;
                thisCube.GetComponent<Renderer>().material.color = thisLayersColor[levelIntegerOfThisCube];

                if (textIsToggledOff == false) ActivateTextOnCube(thisCube);
            }

            nowCubesAreTransparent = false;
            noMoreTransparencyNeeded = false;
        }

        // Case 2a: the first time a column is selected; sets all other cubes to transparent and without labels
        if (transparentOrNot && noMoreTransparencyNeeded == false)
        {
            foreach (GameObject thisCube in totalCubesInMatrix)
            {
                thisCube.GetComponent<Renderer>().material = transparentColor;
                DeActivateTextOnCube(thisCube);
            }

            nowCubesAreTransparent = true;
            noMoreTransparencyNeeded = true;
        }

        // Case 2b: once in transparency mode; only turns the previously selected cubes transparent (to save on processing)
        else if (transparentOrNot && noMoreTransparencyNeeded == true)
        {
            foreach (GameObject thisCube in selectedCubes)
            {
                thisCube.GetComponent<Renderer>().material = transparentColor;
                DeActivateTextOnCube(thisCube);

            }

            nowCubesAreTransparent = true;

        }


        foreach (GameObject thisCube in theseCubes)
        {
            thisCube.GetComponent<Renderer>().material = activeColor;
            ActivateTextOnCube(thisCube);
        }

        selectedCubes = theseCubes;

    }



    public void ActivateTextOnCube(GameObject thisCube)
    {
        thisCube.transform.Find("squareNumber").gameObject.SetActive(true);
        thisCube.transform.Find("cubeLetter").gameObject.SetActive(true);
    }

    public void DeActivateTextOnCube(GameObject thisCube)
    {
        thisCube.transform.Find("squareNumber").gameObject.SetActive(false);
        thisCube.transform.Find("cubeLetter").gameObject.SetActive(false);
    }

    public void DeActivateTextOnAllCubes()
    {
        foreach (GameObject thisCube in totalCubesInMatrix)
        {
            thisCube.transform.Find("squareNumber").gameObject.SetActive(textIsToggledOff);
            thisCube.transform.Find("cubeLetter").gameObject.SetActive(textIsToggledOff);
        }

        textIsToggledOff = !textIsToggledOff;

    }

    public void HideSquares()
    {

        renderSmartSquareArray[currentLayer].SetActive(false);

        foreach (GameObject cube in blocksToDisableToDecreaseMemoryLoad[currentLayer])
        {
            cube.SetActive(false);
        }
        if (currentLayer > 0)
        {
            renderSmartSquareArray[currentLayer - 1].SetActive(false);
            foreach (GameObject cube in blocksToDisableToDecreaseMemoryLoad[currentLayer - 1])
            {
                cube.SetActive(true);
            }
        }

    }

    public void ShowSquares()
    {
        renderSmartSquareArray[currentLayer].SetActive(false);

        if (currentLayer < matrixDepth - 1 && currentLayer > 0)
        {
            foreach (GameObject cube in blocksToDisableToDecreaseMemoryLoad[currentLayer - 1])
            {
                cube.SetActive(false);
            }
            renderSmartSquareArray[currentLayer - 1].SetActive(true);
        }

        foreach (GameObject cube in blocksToDisableToDecreaseMemoryLoad[currentLayer])
        {
            cube.SetActive(true);
        }

    }

    //TODO fix column selection so that it works with visible squares and then de-activates them when the column goes away



    // This Coroutine does fancy box stacking at the start of the program, just for visual effect, nothing more
    IEnumerator StackBoxes()
    {
        for (int f = 0; f < cubesInMatrix.Length + 5; f = f + 3)
        {
            if (f < cubesInMatrix.Length)
            {
                cubesInMatrix[f].SetActive(true);
            }
            else { }

            if (f + 1 < cubesInMatrix.Length)
            {
                cubesInMatrix[f + 1].SetActive(true);
            }
            else { }

            if (f + 2 < cubesInMatrix.Length)
            {
                cubesInMatrix[f + 2].SetActive(true);
            }
            else { }


            yield return null;
        }
    }

}
