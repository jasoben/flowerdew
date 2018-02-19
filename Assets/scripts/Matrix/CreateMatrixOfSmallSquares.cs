﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMatrixOfSmallSquares : MonoBehaviour
{
    //Colors
    private Color[] blockColor;

    //Custom Classes
    private CubeBuffer cubeBuffer;

    //GameObjects
    private GameObject[] cubesInMatrix;
    private GameObject matrixCube;
    private GameObject masterObject;

    //Numbers
    private int matrixXSize;
    private int matrixZSize;
    private int currentCubeNumber;
    private float matrixCubeXZScale;
    private float interCubeDistance;
    private int largeSquareNumber;
    public int LargeSquareNumber
    {
        get
        {
            return largeSquareNumber;
        }
        set { largeSquareNumber = value; }
    }
    private int largeSquareXPosition;
    public int LargeSquareXPosition
    {
        get
        {
            return largeSquareXPosition;
        }
        set { largeSquareXPosition = value; }
    }
    private int largeSquareZPosition;
    public int LargeSquareZPosition
    {
        get
        {
            return largeSquareZPosition;
        }
        set { largeSquareZPosition = value; }
    }
    private int depth;
    public int Depth
    {
        get
        {
            return depth;
        }
        set { depth = value; }
    }

    //Vectors
    private Vector3 matrixLocation;
    private Vector3 matrixCubeOffset;

   
    // Use this for initialization
    void Awake()
    {

        //Colors
        blockColor = new Color[8];
        float baseColor = .3f;
        float colorMultiplier = 12;
        for (int i = 0; i < 8; i++)
        {

            blockColor[7 - i] = new Color(baseColor + (i + 1) / colorMultiplier, baseColor + (i + 1) / colorMultiplier, baseColor + (i + 1) / colorMultiplier);            
        }
        

        //GameObjects
        masterObject = GameObject.Find("MasterObject");
        matrixCube = masterObject.GetComponent<MasterScript>().SmallSquarePrefab;
        //cubesInMatrix = defined in LATE

        //Numbers
        matrixXSize = MasterScript.MatrixXSize;
        matrixZSize = MasterScript.MatrixZSize;
        matrixCubeXZScale = MasterScript.MatrixCubeXZScale;
        interCubeDistance = MasterScript.InterCubeDistance;
        currentCubeNumber = 0;

        //LATE
        cubesInMatrix = new GameObject[matrixXSize * matrixZSize];
                
    }

    void Update()
    {

    }

    public void CreateTheMatrixOfSmallSquares(int largeSquareNumber, int depth, GameObject parentLayerObject)
    {

        float adjustedDepth = matrixLocation.y;

        for (int z = 0; z < matrixZSize; z++)
        {

            for (int x = 0; x < matrixXSize; x++)
            {
                matrixCubeOffset = new Vector3((matrixCubeXZScale * x) + interCubeDistance, adjustedDepth, (matrixCubeXZScale * z + 1) + interCubeDistance);

                Vector3 newPosition = transform.position + matrixCubeOffset;
                cubesInMatrix[currentCubeNumber] = Instantiate(matrixCube, newPosition, Quaternion.identity, transform);
                string cubeName = "littleSquare" + "_alpha" + currentCubeNumber + "_" + largeSquareNumber + "_" + depth;

                cubeName = ChangeNumberToLetter(cubeName);

                cubesInMatrix[currentCubeNumber].GetComponent<Renderer>().material.SetColor("_Color", blockColor[depth]);
                cubesInMatrix[currentCubeNumber].name = cubeName;

                //Insert text on each child element (Text Rendereres)
                cubesInMatrix[currentCubeNumber].transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = largeSquareNumber.ToString();
                cubesInMatrix[currentCubeNumber].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "alpha" + currentCubeNumber.ToString();
                cubesInMatrix[currentCubeNumber].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = ChangeNumberToLetter(cubesInMatrix[currentCubeNumber].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text);
                cubesInMatrix[currentCubeNumber].transform.GetChild(2).gameObject.GetComponent<TextMesh>().text = depth.ToString();

                cubesInMatrix[currentCubeNumber].tag = "level" + depth;

                ClickOnCube thisCube = cubesInMatrix[currentCubeNumber].GetComponent<ClickOnCube>();

                MasterScript.AllCubes.Add(cubesInMatrix[currentCubeNumber]);

                currentCubeNumber++;

            }
        }

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
    
}
