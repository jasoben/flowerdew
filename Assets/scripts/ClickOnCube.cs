using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour {

    public string myNameIs;
    public Text cubeInfo;


    public Material activeColor;
    public Material passiveColor;

    private CreateMatrix matrix;

    // Use this for initialization
    void Start () {
        cubeInfo = GameObject.Find("CubeInfo").GetComponent<Text>();
        matrix = GameObject.Find("CubeDuplicator").GetComponent<CreateMatrix>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        Debug.Log(this.gameObject);
        Debug.Log(matrix.currentCube);
        this.GetComponent<Renderer>().material = activeColor;
        if (matrix.currentCube != null) matrix.currentCube.GetComponent<Renderer>().material = passiveColor;
        cubeInfo.text = myNameIs;
        matrix.currentCube = this.gameObject;

        Application.ExternalCall("find_content", myNameIs);
    }

}
