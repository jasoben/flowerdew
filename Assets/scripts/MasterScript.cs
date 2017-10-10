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

    private GameObject largeSquareObject;

    //Numbers
    private int matrixDepth;
    public int MatrixDepth
    {
        get
        {
            return matrixDepth;
        }
    }
    // Use this for initialization
    void Start () {

        //Numbers
        matrixDepth = 8;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendCubesToExternalApplication(List<GameObject> selectedCubes)
    {
        Application.ExternalCall("find_content", selectedCubes);                
    }
    

}
