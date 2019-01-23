using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerNavigator : MonoBehaviour {

    //Event handling delegates
    public delegate void LayerChanged();
    public static LayerChanged LayerHasBeenChanged;

    //GameObjects
    private static GameObject[][] layerObjects;
    private static List<GameObject> visibleObjects;
    private static List<GameObject> inVisibleObjects;

    //Numbers
    private static int currentlayer;
    public static int CurrentLayer
    {
        get { return currentlayer; }
        set { currentlayer = value; }
    }

    private void Awake()
    {
        currentlayer = 0;
        CubeBuffer.CubesAreCreated += RunWhenObjectsCreated;
    }
    // Use this for initialization
    void Start () {
        visibleObjects = new List<GameObject>();
        inVisibleObjects = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RunWhenObjectsCreated()
    {
        
        layerObjects = new GameObject[8][];
        for (int i = 0; i < 8; i++)
        {
            layerObjects[i] = GameObject.FindGameObjectsWithTag("level" + i);
            
            //Add cubes to "visible cubes" and "invisible cubes" lists to increase performance in WebGL
            foreach (GameObject thisObject in layerObjects[i])
            {
                string bigSquareNumber = thisObject.transform.GetChild(0).GetComponent<TextMesh>().text;
                string littleSquareLetter = thisObject.transform.GetChild(1).GetComponent<TextMesh>().text;
                string depth = thisObject.transform.GetChild(2).GetComponent<TextMesh>().text;
                
                if ((bigSquareNumber == "386" ||
                    bigSquareNumber == "316" ||
                    bigSquareNumber == "247" ||
                    bigSquareNumber == "177") &&
                    (littleSquareLetter == "A" ||
                    littleSquareLetter == "G" ||
                    littleSquareLetter == "N" ||
                    littleSquareLetter == "U"))
                {
                    visibleObjects.Add(thisObject);
                }
                else if ((bigSquareNumber == "180" ||
                    bigSquareNumber == "386" ||
                    bigSquareNumber == "387" ||
                    bigSquareNumber == "388" ||
                    bigSquareNumber == "389") &&
                    (littleSquareLetter == "U" ||
                    littleSquareLetter == "V" ||
                    littleSquareLetter == "W" ||
                    littleSquareLetter == "X" ||
                    littleSquareLetter == "Y" ||
                    littleSquareLetter == "Z"))
                {
                    visibleObjects.Add(thisObject);
                }
                else if ((bigSquareNumber == "180" ||
                    bigSquareNumber == "316" ||
                    bigSquareNumber == "177" ||
                    bigSquareNumber == "178" ||
                    bigSquareNumber == "179") &&
                    (littleSquareLetter == "A" ||
                    littleSquareLetter == "B" ||
                    littleSquareLetter == "C" ||
                    littleSquareLetter == "D" ||
                    littleSquareLetter == "E" ||
                    littleSquareLetter == "F"))
                {
                    visibleObjects.Add(thisObject);
                }
                else if ((bigSquareNumber == "180" ||
                    bigSquareNumber == "249" ||
                    bigSquareNumber == "319" ||
                    bigSquareNumber == "389") &&
                    (littleSquareLetter == "F" ||
                    littleSquareLetter == "M" ||
                    littleSquareLetter == "T" ||
                    littleSquareLetter == "Z"))
                {
                    visibleObjects.Add(thisObject);
                }
                
                else
                {
                    thisObject.SetActive(false);
                    inVisibleObjects.Add(thisObject);
                }

            }
        }
        //Re-enable layer 0
        foreach (GameObject thisObject in layerObjects[0])
        {
            thisObject.SetActive(true);
        }

        CubeBuffer.CubesAreCreated -= RunWhenObjectsCreated;
    }

    public static void ChangeLayerTo(int layerNumber)
    {
        layerNumber = (int)Mathf.Clamp((float)layerNumber, 0, 7);
        currentlayer = layerNumber;
        LayerHasBeenChanged();
    }

    public static void ActivateOrDeactivateLayer(int layerNumber, bool yesOrNo)
    {
        DisableHiddenCubes();
        layerNumber = (int)Mathf.Clamp((float)layerNumber, 0, 7);
        foreach (GameObject thisObject in layerObjects[layerNumber])
        {
            thisObject.SetActive(yesOrNo);
        }
        ReActivateSelectedCubes();
        TurnOffBigCubesInBuffer();
        
    }
    public static void ClearCubesAboveLayer()
    {
        for (int i = CurrentLayer - 1; i > -1; i--)
        {
            foreach (GameObject thisObject in layerObjects[i])
            {
                thisObject.SetActive(false);
            }
        }
    }
    public static void DisableHiddenCubes()
    {
        foreach (GameObject thisObject in inVisibleObjects)
        {
            thisObject.SetActive(false);
            thisObject.GetComponent<Renderer>().material.color = MasterScript.DefaultCubeColor;
        }
    }
    private static void ReActivateSelectedCubes()
    {
        foreach (GameObject thisObject in CubeBuffer.SelectedCubes)
        {
            thisObject.SetActive(true);
            thisObject.GetComponent<Renderer>().material.color = MasterScript.SelectedCubeColor;
        }
    }
    private static void TurnOffBigCubesInBuffer()
    {
        foreach (GameObject thisObject in MasterScript.LastSelectedBigCube)
        {
            thisObject.SetActive(false);
        }
    }
}
