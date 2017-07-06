﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMatrix : MonoBehaviour {

    public int matrixWidth;
    public int matrixHeight;
    public int matrixDepth;

    private float layerDepthColorGradientSpread;
    private float layerDepthColorStartingValue;

    public GameObject matrixCube;
    public GameObject matrixCenter;

    public Material groundColor;
    private Color newBlockColor;

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

    private List<GameObject> otherCubes;

    public List<GameObject> OtherCubes
    {
        get
        {
            return otherCubes;
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

        layerDepthColorGradientSpread = .05f;
        layerDepthColorStartingValue = .5f;

        matrixSize = matrixWidth * matrixHeight * matrixDepth;

        cubesInMatrix = new GameObject[matrixSize];
        totalCubesInMatrix = new List<GameObject>();
        otherCubes = new List<GameObject>();

        cubeLayers = new GameObject[matrixDepth][];

        matrixOrigin = new Vector3(FixNumber(matrixWidth), FixNumber(matrixHeight), FixNumber(matrixDepth));

        matrixCenter.transform.position = matrixOrigin;

        currentCubeColumn = new GameObject[matrixDepth];

        currentLayer = matrixDepth - 1;
        
        for (int d = 0; d < matrixDepth; d++)
        {

            int e = d + 1;

            if (d > 2)
            {
                newBlockColor = new Color(layerDepthColorStartingValue * .3f * e, layerDepthColorStartingValue * .3f * e, layerDepthColorStartingValue * .5f * e);
            }
            else if (d < 2)
            {
                newBlockColor = new Color(layerDepthColorStartingValue * .5f * e, layerDepthColorStartingValue * .1f * e, layerDepthColorStartingValue * .1f * e);
            }

            GameObject[] largeSquare387 = CreateLargeSquare(387, 0, 0, d);
            GameObject[] largeSquare388 = CreateLargeSquare(388, 1, 0, d);
            GameObject[] largeSquare389 = CreateLargeSquare(389, 2, 0, d);

            GameObject[] largeSquare317 = CreateLargeSquare(317, 0, 1, d);
            GameObject[] largeSquare318 = CreateLargeSquare(318, 1, 1, d);
            GameObject[] largeSquare319 = CreateLargeSquare(319, 2, 1, d);

            GameObject[] largeSquare247 = CreateLargeSquare(247, 0, 2, d);
            GameObject[] largeSquare248 = CreateLargeSquare(248, 1, 2, d);
            GameObject[] largeSquare249 = CreateLargeSquare(249, 2, 2, d);

            GameObject[] largeSquare177 = CreateLargeSquare(177, 0, 3, d);
            GameObject[] largeSquare178 = CreateLargeSquare(178, 1, 3, d);
            GameObject[] largeSquare179 = CreateLargeSquare(179, 2, 3, d);


            currentLayerIndicatorBlock = Instantiate(layerIndicatorBlock, new Vector3(11, d * matrixCubeDepth * interCubeDistance, 22), Quaternion.identity);
            currentLayerIndicatorBlock.GetComponent<Renderer>().material.color = newBlockColor;
            currentLayerIndicatorBlock.transform.Find("LayerIndicatorText").GetComponent<TextMesh>().text = "Level " + d;
            currentLayerIndicatorBlock.tag = "level" + d;

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

        Debug.Log(cubeText);
        //for (int y = 0; y < matrixDepth; y++)
        //{

        //    cubeLayers[y] = new GameObject[matrixHeight * matrixWidth];

        //

        //    j = 0;

        //}


        // This Coroutine does fancy box stacking at the start of the program, just for visual effect, nothing more

        //  StartCoroutine("StackBoxes");

    }

        // Update is called once per frame
    void Update ()
    {

        
        // Controls

        if (Input.GetKeyDown(moveDownLayer) == true)
        {
            for (int i = 0; i < cubeLayers[currentLayer].Length; i++)
            {
                cubeLayers[currentLayer][i].SetActive(false);
            }

            currentLayer--;

            if (currentLayer < 0)
            {
                currentLayer = 0;
            }
        }
        else if (Input.GetKeyDown(moveUpLayer) == true)
        {
            

            Debug.Log(currentLayer);

            for (int i = 0; i < cubeLayers[currentLayer].Length; i++)
            {
                cubeLayers[currentLayer][i].SetActive(true);
            }

            currentLayer++;

            if (currentLayer > matrixDepth - 1)
            {
                currentLayer = matrixDepth - 1;
            }
        }

        
        
    }

    // This method creates the cubes in a large box

    public GameObject[] CreateLargeSquare(int bigSquareNumber, int bigSquareXLocation, int bigSquareYLocation, int bigSquareDepth)
    {
        int i = 0;
        int j = 0;


        for (int z = 0; z < matrixHeight; z++)
        {

            for (int x = 0; x < matrixWidth; x++)
            {
                cubesInMatrix[i] = Instantiate(matrixCube, new Vector3((bigSquareXLocation * interCubeDistance * interBigSquareDistance * matrixCubeWidth) + x * interCubeDistance, bigSquareDepth * matrixCubeDepth * interCubeDistance, (bigSquareYLocation * interCubeDistance * (interBigSquareDistance-1.7f) * matrixCubeHeight) + z * interCubeDistance), Quaternion.identity) as GameObject;

                totalCubesInMatrix.Add(cubesInMatrix[i]);

                string cubeName = "littleSquare" + "_alpha" + j + "_" + bigSquareNumber + "_" + bigSquareDepth;

                cubeName = ChangeNumberToLetter(cubeName);

                cubesInMatrix[i].GetComponent<Renderer>().material.color = newBlockColor;
                cubesInMatrix[i].name = cubeName;
                cubesInMatrix[i].transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = bigSquareNumber.ToString();
                cubesInMatrix[i].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "alpha" + j.ToString();
                cubesInMatrix[i].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = ChangeNumberToLetter(cubesInMatrix[i].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text);

                //TODO change color of text based on which square it is

                cubesInMatrix[i].tag = "level" + bigSquareDepth;
                
                ClickOnCube thisCube = cubesInMatrix[i].GetComponent<ClickOnCube>();
                thisCube.myNameIs = cubesInMatrix[i].name;

                // cubesInMatrix[i].SetActive(false);

                // cubeLayers[y][j] = cubesInMatrix[i];

                i++;
                j++;

            }
        }

        return cubesInMatrix;        
    }

    // This method helps calculate the origin of the total grid by running the code through a simple equation

    public float FixNumber (int matrixDimension)
    {
        float newMatrixDimension = (matrixDimension - 1) * interCubeDistance / 2;
        return newMatrixDimension;
    }


    private string ChangeNumberToLetter (string thisName)
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
