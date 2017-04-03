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
    public GameObject[][] cubeLayers = new GameObject[288][];

    public GameObject currentCube;

    public string moveUpLayer;
    public string moveDownLayer;

    private int currentLayer;

    // Use this for initialization
    void Start()
    {

        matrixCenter = this.gameObject;

        matrixCubeHeight = matrixCubeWidth = 1;
        matrixCubeDepth = 1;

        matrixWidth = 6;
        matrixHeight = 4;

        matrixSize = matrixWidth * matrixHeight;

        cubesInMatrix = new GameObject[matrixSize];

        matrixOrigin = new Vector3(FixNumber(matrixWidth), FixNumber(matrixHeight), FixNumber(matrixDepth));

        matrixCenter.transform.position = matrixOrigin;



        currentLayer = matrixDepth - 1;

        for (int d = 0; d < 5; d++)
        {
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
        }

        foreach (GameObject specificCube in cubesInMatrix)
        { 
            cubeLayers[0] = GameObject.FindGameObjectsWithTag("level0");
            cubeLayers[1] = GameObject.FindGameObjectsWithTag("level1");
            cubeLayers[2] = GameObject.FindGameObjectsWithTag("level2");
            cubeLayers[3] = GameObject.FindGameObjectsWithTag("level3");
            cubeLayers[4] = GameObject.FindGameObjectsWithTag("level4");
        }

        Debug.Log(cubeLayers[0]);

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


        for (int z = 0; z < matrixHeight; z++)
        {

            for (int x = 0; x < matrixWidth; x++)
            {
                cubesInMatrix[i] = Instantiate(matrixCube, new Vector3((bigSquareXLocation * interCubeDistance * 6 * matrixCubeWidth) + x * interCubeDistance, bigSquareDepth * matrixCubeDepth * interCubeDistance, (bigSquareYLocation * interCubeDistance * 4 * matrixCubeHeight) + z * interCubeDistance), Quaternion.identity) as GameObject;

                string cubeName = "littleSquare" + "_alpha" + j + "_" + bigSquareNumber + "_" + bigSquareDepth;

                cubeName = cubeName.Replace("alpha18", "A");
                cubeName = cubeName.Replace("alpha19", "B");
                cubeName = cubeName.Replace("alpha20", "C");
                cubeName = cubeName.Replace("alpha21", "D");
                cubeName = cubeName.Replace("alpha22", "E");
                cubeName = cubeName.Replace("alpha23", "F");

                cubeName = cubeName.Replace("alpha12", "G");
                cubeName = cubeName.Replace("alpha13", "H");
                cubeName = cubeName.Replace("alpha14", "J");
                cubeName = cubeName.Replace("alpha15", "K");
                cubeName = cubeName.Replace("alpha16", "L");
                cubeName = cubeName.Replace("alpha17", "M");

                cubeName = cubeName.Replace("alpha6", "N");
                cubeName = cubeName.Replace("alpha7", "P");
                cubeName = cubeName.Replace("alpha8", "Q");
                cubeName = cubeName.Replace("alpha9", "R");
                cubeName = cubeName.Replace("alpha10", "S");
                cubeName = cubeName.Replace("alpha11", "T");

                cubeName = cubeName.Replace("alpha0", "U");
                cubeName = cubeName.Replace("alpha1", "V");
                cubeName = cubeName.Replace("alpha2", "W");
                cubeName = cubeName.Replace("alpha3", "X");
                cubeName = cubeName.Replace("alpha4", "Y");
                cubeName = cubeName.Replace("alpha5", "Z");

                cubesInMatrix[i].name = cubeName;
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
