using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MasterScript : MonoBehaviour {

    //Network Communication
    [DllImport("__Internal")]
    private static extern void SendData(string str);

    //Event handling delegates
    public delegate void AllObjectsHaveBeenCreated();
    public static AllObjectsHaveBeenCreated ObjectsDone;

    //GameObjects
    [SerializeField]
    private GameObject smallSquarePrefab;
    public GameObject SmallSquarePrefab
    {
        get { return smallSquarePrefab; }
    }
    [SerializeField]
    private GameObject largeSquareObject;
    public GameObject LargeSquareObject
    {
        get { return largeSquareObject; }
    }
    [SerializeField]
    private GameObject indicatorObject;
    public GameObject IndicatorObject
    {
        get { return indicatorObject; }
    }
    [SerializeField]
    private GameObject layerEmptyObject;
    public GameObject LayerEmptyObject
    {
        get { return layerEmptyObject; }
    }

    //this stores a buffer of the last selected big cube, to re-activate it when "mouse out"
    //I'm doing it this way because I'm deactivating the cube when I "mouse over", which means no "mouse out" would register
    //I'm doing it that way because I need to have cursor activation for the stuff behind the big cube
    public static List<GameObject> LastSelectedBigCube;
    public static bool CubeClickedWhileBigCubeHidden; //this is a way to determine whether the 
    //big cube should stay hidden after selection of cubes behind it

    private static List<GameObject> allCubes;
    public static List<GameObject> AllCubes
    {
        get
        {
            return allCubes;
        }
        set { allCubes = value; }
    }
    public static List<GameObject> AllBigCubes { get; set; } // these are the cubes that have number and layer info on them
    private GameObject thisLargeSquare;
    private GameObject[] thisLayer;

    //Numbers
    private static int matrixDepth;
    public static int MatrixDepth
    {
        get
        { return matrixDepth; }
    }
    private static int matrixXSize;
    public static int MatrixXSize
    {
        get { return matrixXSize; }
    }
    private static int matrixZSize;
    public static int MatrixZSize
    {
        get { return matrixZSize; }
    }
    private static int matrixCubeXZScale;
    public static int MatrixCubeXZScale
    {
        get { return matrixCubeXZScale; }
    }
    private static float interCubeDistance;
    public static float InterCubeDistance
    {
        get { return interCubeDistance; }
    }
    private static float largeSquareSeparationX;
    private static float largeSquareSeparationZ;

    //Vectors
    private Vector3 newLargeSquarePosition;

    //Colors
    [SerializeField]
    private Color defaultCubeColor, selectedCubeColor, highlightColor;

    public static Color DefaultCubeColor, SelectedCubeColor, HighlightColor;

    // Use this for initialization
    void Start () {

        //Colors
        DefaultCubeColor = defaultCubeColor;
        SelectedCubeColor = selectedCubeColor;
        HighlightColor = highlightColor;
        
        //Numbers
        matrixDepth = 8;
        matrixXSize = 6;
        matrixZSize = 4;
        matrixCubeXZScale = 1;
        interCubeDistance = 0f;
        largeSquareSeparationX = (matrixCubeXZScale * interCubeDistance) + (matrixXSize * matrixCubeXZScale);
        largeSquareSeparationZ = (matrixCubeXZScale * interCubeDistance) + (matrixZSize * matrixCubeXZScale);

        //GameObjects

        thisLayer = new GameObject[matrixDepth];
        allCubes = new List<GameObject>();
        AllBigCubes = new List<GameObject>();

        LastSelectedBigCube = new List<GameObject>();

        for (int d = 0; d < matrixDepth; d++)
        {
            thisLayer[d] = Instantiate(layerEmptyObject, this.transform.position, Quaternion.identity);
            thisLayer[d].name = "Layer " + d;

            CreateLargeSquare(386, -1, 0, d);
            CreateLargeSquare(387, 0, 0, d);
            CreateLargeSquare(388, 1, 0, d);
            CreateLargeSquare(389, 2, 0, d);

            CreateLargeSquare(316, -1, 1, d);
            CreateLargeSquare(317, 0, 1, d);
            CreateLargeSquare(318, 1, 1, d);
            CreateLargeSquare(319, 2, 1, d);

            CreateLargeSquare(247, 0, 2, d);
            CreateLargeSquare(248, 1, 2, d);
            CreateLargeSquare(249, 2, 2, d);

            CreateLargeSquare(177, 0, 3, d);
            CreateLargeSquare(178, 1, 3, d);
            CreateLargeSquare(179, 2, 3, d);
            CreateLargeSquare(180, 3, 3, d);

        }

        indicatorObject.SetActive(false);

        ObjectsDone();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void SendCubesToExternalApplication(string thisString)
    {
        SendData(thisString);                

        //Application.ExternalCall("ReceiveUnityData", thisString);
    }
    
    //This method creates the "big squares" that hold the 6x4 matrix of "small squares"
    public void CreateLargeSquare(int largeSquareNumber, int largeSquareXPosition, int largeSquareZPosition, int depth)
    {
        float depthAdjuster = 0.3f;
        float adjustedDepth = depthAdjuster * depth;
        
        newLargeSquarePosition = new Vector3(largeSquareXPosition * largeSquareSeparationX, -adjustedDepth, largeSquareZPosition * largeSquareSeparationZ);
        newLargeSquarePosition = ConvertWorldSquareLocationToLocal(newLargeSquarePosition);

        thisLargeSquare = Instantiate(largeSquareObject, newLargeSquarePosition, Quaternion.identity);
        thisLargeSquare.name = "Large Square " + largeSquareNumber;

        thisLargeSquare.transform.SetParent(thisLayer[depth].transform);

        thisLargeSquare.GetComponent<CreateMatrixOfSmallSquares>().CreateTheMatrixOfSmallSquares(largeSquareNumber, depth, thisLargeSquare);
    }

    public Vector3 ConvertWorldSquareLocationToLocal(Vector3 naiveVector)
    {
        Vector3 adjustedPosition = new Vector3(naiveVector.x + this.transform.position.x, naiveVector.y + this.transform.position.y, naiveVector.z + this.transform.position.z);
        return adjustedPosition;
    }

}
