using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMatrixOfSmallSquares : MonoBehaviour
{
    //Colors
    private Color[] blockColor;

    //GameObjects
    private GameObject[] cubesInMatrix;
    private GameObject matrixCube;
    private GameObject matrixOrigin;

    //Numbers
    private int matrixXSize;
    private int matrixZSize;
    private int currentCubeNumber;
    private float matrixCubeXZScale;
    private float interCubeDistance;

    //Vectors
    private Vector3 matrixLocation;
    private Vector3 matrixCubeOffset;

    // Use this for initialization
    private void Start()
    {

        //Colors
        blockColor = new Color[8];

        blockColor[0] = new Color(.1f, .1f, .1f);
        blockColor[1] = new Color(.2f, .2f, .2f);
        blockColor[2] = new Color(.3f, .3f, .3f);
        blockColor[3] = new Color(.4f, .4f, .4f);
        blockColor[4] = new Color(.5f, .5f, .5f);
        blockColor[5] = new Color(.7f, .7f, .7f);
        blockColor[6] = new Color(.8f, .8f, .8f);
        blockColor[7] = new Color(.9f, .9f, .9f);

        //GameObjects
        matrixCube = GameObject.Find("MasterObject").GetComponent<MasterScript>().SmallSquarePrefab;
        cubesInMatrix = new GameObject[24];
        
        //Numbers
        matrixXSize = 6;
        matrixZSize = 4;
        matrixCubeXZScale = 1;
        interCubeDistance = .2f;
        currentCubeNumber = 0;

        //Vectors
        matrixLocation = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                
    }

    private void Update()
    {
        
    }

    public CreateMatrixOfSmallSquares(int largeSquareNumber, int xLocation, int zLocation, int depth)
    {
        

        for (int z = 0; z < matrixZSize; z++)
        {

            for (int x = 0; x < matrixXSize; x++)
            {
                matrixCubeOffset = new Vector3((xLocation * matrixCubeXZScale * x) + interCubeDistance, depth, (zLocation * matrixCubeXZScale * z) + interCubeDistance);

                cubesInMatrix[currentCubeNumber] = Instantiate(matrixCube, matrixLocation + matrixCubeOffset, Quaternion.identity);
                
                string cubeName = "littleSquare" + "_alpha" + currentCubeNumber + "_" + largeSquareNumber + "_" + depth;

                cubeName = ChangeNumberToLetter(cubeName);

                cubesInMatrix[currentCubeNumber].GetComponent<Renderer>().material.color = blockColor[currentCubeNumber];
                cubesInMatrix[currentCubeNumber].name = cubeName;
                cubesInMatrix[currentCubeNumber].transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = largeSquareNumber.ToString();
                cubesInMatrix[currentCubeNumber].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "alpha" + currentCubeNumber.ToString();
                cubesInMatrix[currentCubeNumber].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = ChangeNumberToLetter(cubesInMatrix[currentCubeNumber].transform.GetChild(1).gameObject.GetComponent<TextMesh>().text);

                cubesInMatrix[currentCubeNumber].tag = "level" + largeSquareNumber;

                ClickOnCube thisCube = cubesInMatrix[currentCubeNumber].GetComponent<ClickOnCube>();
                thisCube.myNameIs = cubesInMatrix[currentCubeNumber].name;

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
