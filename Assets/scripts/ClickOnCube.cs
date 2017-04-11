using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour {

    public string myNameIs;
    public Text cubeInfo;

    private GameObject[] cubeColumn;
    private List<GameObject> otherCubes;

    public Material activeColor;
    public Material passiveColor;
    public Material transparentColor;

    private CreateMatrix matrix;

    private string cubeNameWithoutDepth;
  
    // Use this for initialization
    void Start () {
        cubeInfo = GameObject.Find("CubeInfo").GetComponent<Text>();
        matrix = GameObject.Find("CubeDuplicator").GetComponent<CreateMatrix>();

        cubeColumn = new GameObject[5];
        otherCubes = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnMouseDown()
    {

        foreach(GameObject thisCube in matrix.totalCubesInMatrix)
        {
            otherCubes.Add(thisCube);
        }
        
        

        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            matrix.OtherCubes.Clear();

            for (int d = 0; d < 5; d++)
            {

                cubeNameWithoutDepth = this.name.Remove(this.name.Length - 1, 1);
                
                foreach (GameObject cubeWithSimilarName in matrix.totalCubesInMatrix)
                {

                    if (cubeWithSimilarName.name.Contains(cubeNameWithoutDepth + d.ToString()))
                    {
                        cubeColumn[d] = cubeWithSimilarName;
                    }
                    
                }

                otherCubes.Remove(cubeColumn[d]);

            }

            foreach (GameObject thisCube in otherCubes)
            {
                matrix.OtherCubes.Add(thisCube);
            }
                        

            if (matrix.CurrentCubeColumn[0] != null)
            {
                foreach (GameObject currentColumn in matrix.CurrentCubeColumn)
                {
                    currentColumn.GetComponent<Renderer>().material = passiveColor;
                }
            }

            if (matrix.currentCube != null)
            {
                matrix.currentCube.GetComponent<Renderer>().material = passiveColor;
                matrix.currentCube = null;
            }


            for (int y = 0; y < cubeColumn.Length; y++)
            {
                matrix.CurrentCubeColumn[y] = cubeColumn[y];
            }

            foreach (GameObject hideThisCube in matrix.OtherCubes)
            {
                hideThisCube.GetComponent<Renderer>().material = transparentColor;
            }

            foreach (GameObject selectedColumnCube in cubeColumn)
            {
                selectedColumnCube.GetComponent<Renderer>().material = activeColor;
            }


        }
        
        else
        {

            foreach (GameObject showThisCube in matrix.OtherCubes)
            {
                showThisCube.GetComponent<Renderer>().material = passiveColor;
            }

            this.GetComponent<Renderer>().material = activeColor;
            if (matrix.currentCube != null) matrix.currentCube.GetComponent<Renderer>().material = passiveColor;
            cubeInfo.text = myNameIs;
            matrix.currentCube = this.gameObject;

            Application.ExternalCall("find_content", myNameIs);

            

            if (matrix.CurrentCubeColumn[0] != null)
            {
                foreach (GameObject currentColumn in matrix.CurrentCubeColumn)
                {
                    currentColumn.GetComponent<Renderer>().material = passiveColor;
                }
                for (int y = 0; y < matrix.CurrentCubeColumn.Length; y++)
                {
                    matrix.CurrentCubeColumn[y] = null;
                }
            }

        }

        
        

    }

   

}
