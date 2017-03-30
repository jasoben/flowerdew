using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMatrix : MonoBehaviour {

    public int matrixWidth;
    public int matrixHeight;
    public int matrixDepth;

    public GameObject matrixCube;
    public GameObject matrixCenter;
    
    private int matrixSize;
    
    public Vector3 matrixOrigin;

    private int matrixCubeWidth;
    private int matrixCubeHeight;
    private int matrixCubeDepth;

    private float interCubeDistance = 1.2f; 

    private GameObject[] cubesInMatrix;
    public GameObject[][] cubeLayers = new GameObject[100][];

    public GameObject currentCube;

    public string moveUpLayer;
    public string moveDownLayer;

    private int currentLayer;

    // Use this for initialization
    void Start () {

        matrixCenter = this.gameObject;

        matrixCubeHeight = matrixCubeWidth = matrixCubeDepth = 1;

        matrixWidth = 6;
        matrixHeight = 4;

        matrixSize = matrixWidth * matrixHeight;

        cubesInMatrix = new GameObject[matrixSize];

        matrixOrigin = new Vector3(FixNumber(matrixWidth), FixNumber(matrixHeight), FixNumber(matrixDepth));

        matrixCenter.transform.position = matrixOrigin;

        

        currentLayer = matrixDepth - 1;

        CreateLargeSquare(387, 0, 0, 0);

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
        void Update () {

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

        
        for (int x = 0; x < matrixWidth; x++)
        {

            for (int z = 0; z < matrixHeight; z++)
            {
                cubesInMatrix[i] = Instantiate(matrixCube, new Vector3((bigSquareXLocation * interCubeDistance) + x * interCubeDistance, 0, (bigSquareYLocation * interCubeDistance) + z * interCubeDistance), Quaternion.identity) as GameObject;

                string cubeName = "littleSquare" + "_alpha" + j + "_" + bigSquareNumber + "_" + bigSquareDepth;

                cubeName.Replace("alpha18", "A");
                cubeName.Replace("alpha19", "B");
                cubeName.Replace("alpha20", "C");
                cubeName.Replace("alpha21", "D");
                cubeName.Replace("alpha22", "E");
                cubeName.Replace("alpha23", "F");

                cubeName.Replace("alpha12", "G");
                cubeName.Replace("alpha13", "H");
                cubeName.Replace("alpha14", "J");
                cubeName.Replace("alpha15", "K");
                cubeName.Replace("alpha16", "L");
                cubeName.Replace("alpha17", "M");

                cubeName.Replace("alpha6", "N");
                cubeName.Replace("alpha7", "P");
                cubeName.Replace("alpha8", "Q");
                cubeName.Replace("alpha9", "R");
                cubeName.Replace("alpha10", "S");
                cubeName.Replace("alpha11", "T");

                cubeName.Replace("alpha0", "U");
                cubeName.Replace("alpha1", "V");
                cubeName.Replace("alpha2", "W");
                cubeName.Replace("alpha3", "X");
                cubeName.Replace("alpha4", "Y");
                cubeName.Replace("alpha5", "Z");

                cubesInMatrix[i].name = cubeName;
                
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
