using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeTextEnabler : MonoBehaviour {

    //GameObjects
    private List<GameObject> textObjects;
    public List<GameObject> TextObjects
    {
        get { return textObjects; }        
    }

    private bool textShowingOrNot;

    //Numbers
    private string thisLayer;

	// Use this for initialization
	void Start () {
        LayerNavigator.LayerHasBeenChanged += ShowOrHideTextOnCurrentLayer;
        thisLayer = this.name;
        textObjects = new List<GameObject>();
        textShowingOrNot = true;

        foreach (Transform child in transform)
        {
            foreach (Transform secondChild in child)
            {
                textObjects.Add(secondChild.Find("cubeLetter").gameObject);
                textObjects.Add(secondChild.Find("squareNumber").gameObject);
                textObjects.Add(secondChild.Find("layerNumber").gameObject);
            }
        }

        ShowOrHideTextOnCurrentLayer();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ShowOrHideTextOnCurrentLayer()
    {
        if (textShowingOrNot)
        {
            if (thisLayer == "Layer " + LayerNavigator.CurrentLayer)
            {
                foreach (GameObject thisTextObject in textObjects)
                {
                    thisTextObject.SetActive(true);
                }

            }
            else
            {
                foreach (GameObject thisTextObject in textObjects)
                {
                    thisTextObject.SetActive(false);
                }
            }

            ShowSelectedText();
        }
    }

    public void ShowOrHideText()
    {
        textShowingOrNot = !textShowingOrNot;
        
        foreach (GameObject thisTextObject in textObjects)
        {
            thisTextObject.SetActive(textShowingOrNot);
        }

        ShowSelectedText();
    }

    public void ShowSelectedText()
    {
        if (CubeBuffer.SelectedCubes != null)
        {
            foreach (GameObject thisObject in CubeBuffer.SelectedCubes)
            {
                thisObject.transform.Find("cubeLetter").gameObject.SetActive(true);
                thisObject.transform.Find("squareNumber").gameObject.SetActive(true);
                thisObject.transform.Find("layerNumber").gameObject.SetActive(true);
            }
        }
    }
}
