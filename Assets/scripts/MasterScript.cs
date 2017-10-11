using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour {

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
    private List<GameObject> allCubes;
    public List<GameObject> AllCubes
    {
        get
        {
            return allCubes;
        }
        set { allCubes = value; }
    }
    private GameObject thisLargeSquare;

    //Numbers
    private int matrixDepth;
    public int MatrixDepth
    {
        get
        { return matrixDepth; }
    }
    private int matrixXSize;
    public int MatrixXSize
    {
        get { return matrixXSize; }
    }
    private int matrixZSize;
    public int MatrixZSize
    {
        get { return matrixZSize; }
    }
    private int matrixCubeXZScale;
    public int MatrixCubeXZScale
    {
        get { return matrixCubeXZScale; }
    }
    private float interCubeDistance;
    public float InterCubeDistance
    {
        get { return interCubeDistance; }
    }
    private float largeSquareSeparationX;
    private float largeSquareSeparationZ;

    //Vectors
    private Vector3 newLargeSquarePosition;

    // Use this for initialization
    void Start () {

        //Numbers
        matrixDepth = 8;
        matrixXSize = 6;
        matrixZSize = 4;
        matrixCubeXZScale = 1;
        interCubeDistance = .2f;
        largeSquareSeparationX = matrixXSize + (matrixXSize * interCubeDistance) + (matrixXSize * matrixCubeXZScale);
        largeSquareSeparationZ = matrixZSize + (matrixZSize * interCubeDistance) + (matrixZSize * matrixCubeXZScale);
        allCubes = new List<GameObject>();

        for (int d = 0; d < matrixDepth; d++)
        {
            CreateLargeSquare(387, 0, 0, d);
            CreateLargeSquare(388, 1, 0, d);
            CreateLargeSquare(389, 2, 0, d);

            CreateLargeSquare(317, 0, 1, d);
            CreateLargeSquare(318, 1, 1, d);
            CreateLargeSquare(319, 2, 1, d);

            CreateLargeSquare(247, 0, 2, d);
            CreateLargeSquare(248, 1, 2, d);
            CreateLargeSquare(249, 2, 2, d);

            CreateLargeSquare(177, 0, 3, d);
            CreateLargeSquare(178, 1, 3, d);
            CreateLargeSquare(179, 2, 3, d);
        }



    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendCubesToExternalApplication(List<GameObject> selectedCubes)
    {
        Application.ExternalCall("find_content", selectedCubes);                
    }
    
    public void CreateLargeSquare(int largeSquareNumber, int largeSquareXPosition, int largeSquareZPosition, int depth)
    {
        newLargeSquarePosition = new Vector3(largeSquareXPosition * largeSquareSeparationX, depth, largeSquareZPosition * largeSquareSeparationZ);
        thisLargeSquare = Instantiate(largeSquareObject, newLargeSquarePosition, Quaternion.identity);
        thisLargeSquare.GetComponent<CreateMatrixOfSmallSquares>().CreateTheMatrixOfSmallSquares(largeSquareNumber, largeSquareXPosition, largeSquareZPosition, depth);
    }
}
