using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour {

    public string myNameIs;
    public Text cubeInfo;

    public GameObject[] cubeColumn;
    
    public Material activeColor;
    public Material passiveColor;

    private CreateMatrix matrix;

    private string cubeNameWithoutDepth;
  
    // Use this for initialization
    void Start () {
        cubeInfo = GameObject.Find("CubeInfo").GetComponent<Text>();
        matrix = GameObject.Find("CubeDuplicator").GetComponent<CreateMatrix>();

        cubeColumn = new GameObject[5];
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnMouseDown()
    {
        
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
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
            

            }

            foreach (GameObject selectedColumnCube in cubeColumn)
            {
                selectedColumnCube.GetComponent<Renderer>().material = activeColor;
            }

            if (matrix.CurrentCubeColumn[0] != null)
            {
                foreach (GameObject currentColumn in matrix.CurrentCubeColumn)
                {
                    currentColumn.GetComponent<Renderer>().material = passiveColor;
                }
            }

            if (matrix.currentCube != null) matrix.currentCube.GetComponent<Renderer>().material = passiveColor;

            //TODO fix the cube turn on / off depending on column selection and vice versa (e.g. if you click on a column, you can't then select individual cubes from within that column, and vice versa for columns with cubes)

            for (int y = 0; y < cubeColumn.Length; y++)
            {
                matrix.CurrentCubeColumn[y] = cubeColumn[y];
            }
           
            
        }
        
        else
        {

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
            }

        }

        
        

    }

   

}
