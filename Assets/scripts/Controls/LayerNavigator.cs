using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerNavigator : MonoBehaviour {

    //Event handling delegates
    public delegate void LayerChanged();
    public static LayerChanged LayerHasBeenChanged;

    //GameObjects
    private static GameObject[][] layerObjects;
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
        MasterScript.ObjectsDone += RunWhenObjectsCreated;
    }
    // Use this for initialization
    void Start () {
        
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
        }

        MasterScript.ObjectsDone -= RunWhenObjectsCreated;
    }

    public static void ChangeLayerTo(int layerNumber)
    {
        layerNumber = (int)Mathf.Clamp((float)layerNumber, 0, 7);
        currentlayer = layerNumber;
        LayerHasBeenChanged();
    }

    public static void ActivateOrDeactivateLayer(int layerNumber, bool yesOrNo)
    {
        layerNumber = (int)Mathf.Clamp((float)layerNumber, 0, 6);
        foreach (GameObject thisObject in layerObjects[layerNumber])
        {
            thisObject.SetActive(yesOrNo);
        }
        ReActivateSelectedCubes();
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
    private static void ReActivateSelectedCubes()
    {
        foreach (GameObject thisObject in CubeBuffer.SelectedCubes)
        {
            thisObject.SetActive(true);
        }
    }
}
