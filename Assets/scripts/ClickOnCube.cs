using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour {

    public string myNameIs;
    public Text cubeInfo;

    //This array and list are for selecting a single cube OR a column, and for clearing selection from the un-selected cubes
    private GameObject[] cubeColumn;
    private List<GameObject> otherCubes;

    public Material activeColor;
    public Material passiveColor;
    public Material transparentColor;

    //This is a reference instance of the class that creates the cube matrix, allowing us to easily reference properties of that class. It's here purely for convencience.
    private CreateMatrix matrix;

    private string cubeNameWithoutDepth;
  
    // Use this for initialization
    void Start () {
        //cubeInfo = GameObject.Find("CubeInfo").GetComponent<Text>();
        //Below we put the CreateMatrix class script (attached to the duplicator game object [an empty]) into an instance of that class.
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

        //This puts all cubes into the local otherCubes object
        foreach(GameObject thisCube in matrix.totalCubesInMatrix)
        {
            otherCubes.Add(thisCube);
        }
        
        
        //This is the conditional for selecting columns

        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            //This clears out the OtherCubes property in the matrix class object, which is how we talk to the non-selected cubes; 
            //we need to do this because the scope of the otherCubes variable doesn't extend outside the conditional surrounding this chunk of code

            matrix.OtherCubes.Clear();

            //The following loop checks each layer of the selected cube to see if they have the same name (without the layer number). 
            //This allows us to select the "column" of cubes that have the same name. 

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

                //It's necessary to remove the cube column from the local otherCubes object for later when we turn those local objects transparent
                otherCubes.Remove(cubeColumn[d]);

            }
            
            //This loop adds all the local non-column objects to the external OtherCubes property so that they aren't lost when we exit the wrapping conditional

            foreach (GameObject thisCube in otherCubes)
            {
                matrix.OtherCubes.Add(thisCube);
            }
                        
            /* This conditional checks to see if the external property CurrentCubeColumn has a value in the first position of its array. 
            If it does have a value, this sets those cubes to passive material color. 
            This step clears the previous set of selected cubes when the next set is selected */

            if (matrix.CurrentCubeColumn[0] != null)
            {
                foreach (GameObject currentColumn in matrix.CurrentCubeColumn)
                {
                    currentColumn.GetComponent<Renderer>().material = passiveColor;
                }
            }

            //This conditional does the same thing as the previous conditional, but for a single cube instead of a column. 
            //This takes care of the case where a user has a single cube selected and then selects a column, thus needing the previously selected cube to disapear. 
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
        
        //This is for the condition when SHIFT is not held down, i.e. to select a single cube instead of a column

        else
        {

            foreach (GameObject showThisCube in matrix.OtherCubes)
            {
                showThisCube.GetComponent<Renderer>().material = passiveColor;
            }

            this.GetComponent<Renderer>().material = activeColor;
            if (matrix.currentCube != null) matrix.currentCube.GetComponent<Renderer>().material = passiveColor;
            //cubeInfo.text = myNameIs;
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
