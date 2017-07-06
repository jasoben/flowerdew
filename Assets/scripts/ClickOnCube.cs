using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour {

    public string myNameIs;
    public Text cubeInfo;

    //This array and list are for selecting a single cube OR a column, and for clearing selection from the un-selected cubes
    private GameObject[] cubeColumn;
    private List<GameObject> otherCubes;
    private List<GameObject> wholeLayerOfCubes;

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

        cubeColumn = new GameObject[matrix.matrixDepth];
        otherCubes = new List<GameObject>();
        wholeLayerOfCubes = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnMouseDown()
    {

        //This puts all cubes into the local otherCubes object
        foreach (GameObject thisCube in matrix.totalCubesInMatrix)
        {
            otherCubes.Add(thisCube);
        }


        //This is the conditional for selecting columns

        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            //This clears out the OtherCubes property in the matrix class object, which is how we talk to the non-selected cubes; 
            //we need to use an external variable this because otherwise the scope of the otherCubes variable wouldn't extend outside the conditional surrounding this chunk of code

            matrix.OtherCubes.Clear();

            //The following loop checks each layer of the selected cube to see if they have the same name (without the layer number). 
            //This allows us to select the "column" of cubes that have the same name. 

            for (int d = 0; d < matrix.matrixDepth; d++)
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
        
        //This is the conditional for selecting the whole layer

        else if (Input.GetKey(KeyCode.LeftControl))
        {

            matrix.OtherCubes.Clear();
            otherCubes.Clear();

            foreach (GameObject thisCube in matrix.totalCubesInMatrix)
            {
                otherCubes.Add(thisCube);
            }

            if (wholeLayerOfCubes != null) wholeLayerOfCubes.Clear();
            string levelTagOfThisCube = this.tag.Replace("level", "");
            int levelIntegerOfThisCube = Int32.Parse(levelTagOfThisCube);

            Debug.Log(levelIntegerOfThisCube);

            foreach (GameObject theCubeInThisLayer in matrix.cubeLayers[levelIntegerOfThisCube])
            {
                wholeLayerOfCubes.Add(theCubeInThisLayer);
                theCubeInThisLayer.GetComponent<Renderer>().material = activeColor;
                otherCubes.Remove(theCubeInThisLayer);
            }
            foreach (GameObject thisCube in otherCubes)
            {
                matrix.OtherCubes.Add(thisCube);

            }
            foreach (GameObject thisCube in matrix.OtherCubes)
            {
                thisCube.GetComponent<Renderer>().material = passiveColor;
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

            this.transform.Find("squareNumber").gameObject.SetActive(true);
            this.transform.Find("cubeLetter").gameObject.SetActive(true);

            //TODO make it so the text disapears when you select another cube

            //TODO make it so the text shows on columns when you select them in "text off mode"

            //TODO change it so that the cubes revert back to their layer color

            //TODO add the level indicators into the otherCubes list so they change color as well

            //TODO build the cube clearing function into a proper function!

            //TODO figure out how to load that function into memory so that it doesn't have to run each time

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

        
            Application.ExternalCall("find_content", myNameIs);

        }

        
        

    }

   

}
