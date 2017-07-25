using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnCube : MonoBehaviour {

    public string myNameIs;
    public Text cubeInfo;

    private bool transparentOrNot;
    private bool showAllCubes;

    //This array and list are for selecting a single cube OR a column, and for clearing selection from the un-selected cubes
    private List<GameObject> theseCubes;
    

    //This is a reference instance of the class that creates the cube matrix, allowing us to easily reference properties of that class. It's here purely for convencience.
    private CreateMatrix matrix;

    private string cubeNameWithoutDepth;
  
    // Use this for initialization
    void Start () {
        //cubeInfo = GameObject.Find("CubeInfo").GetComponent<Text>();
        //Below we put the CreateMatrix class script (attached to the duplicator game object [an empty]) into an instance of that class.
        matrix = GameObject.Find("CubeDuplicator").GetComponent<CreateMatrix>();

        theseCubes = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnMouseDown()
    {

        theseCubes.Clear();

        //This is the conditional for selecting columns

        if (Input.GetKey(KeyCode.C))
        {

            transparentOrNot = true;
            showAllCubes = true;

            for (int d = 0; d < matrix.matrixDepth; d++)
            {

                cubeNameWithoutDepth = this.name.Remove(this.name.Length - 1, 1);

                foreach (GameObject cubeWithSimilarName in matrix.totalCubesInMatrix)
                {

                    if (cubeWithSimilarName.name.Contains(cubeNameWithoutDepth + d.ToString()))
                    {
                        theseCubes.Add(cubeWithSimilarName);
                    }

                }

                //TODO fix this function call so that it doesn't always set all cubes to active (for instance, when you're selecting a column at a lower layer)
                //TODO decide whether or not to return the whole column (or just the objects beneath the current layer) when column is selected

            }

        }

        //This is the conditional for selecting the whole layer

        else if (Input.GetKey(KeyCode.LeftShift))
        {

            transparentOrNot = false;
            showAllCubes = false;

            string levelTagOfThisCube = this.tag.Replace("level", "");
            int levelIntegerOfThisCube = Int32.Parse(levelTagOfThisCube);

            foreach (GameObject theCubeInThisLayer in matrix.cubeLayers[levelIntegerOfThisCube])
            {
                theseCubes.Add(theCubeInThisLayer);
                if (theCubeInThisLayer.name.Contains("Layer")) theseCubes.Remove(theCubeInThisLayer);
            }

            

        }
        //This is for the condition when SHIFT is not held down, i.e. to select a single cube instead of a column

        else
        {
            transparentOrNot = false;
            showAllCubes = false;

            //cubeInfo.text = myNameIs;
            theseCubes.Add(this.gameObject);

            
            
            

        }

        Application.ExternalCall("find_content", theseCubes);

        matrix.ActivateCubes(theseCubes, transparentOrNot, showAllCubes);

        //TODO fix problem where selecting whole layer, then selecting same individual cube, leaves whole layer selected
    }

   

}
