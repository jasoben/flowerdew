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

    private List<GameObject> onlyLargeSquareTitles;
    public List<GameObject> OnlyLargeSquareTitles
    {
        get { return onlyLargeSquareTitles; }        
    }

    private bool textShowingOrNot;

    //Numbers
    private string thisLayer;

	// Use this for initialization
	void Start () {
        LayerNavigator.LayerHasBeenChanged += ShowOrHideTextOnCurrentLayer;
        thisLayer = this.name;
        textObjects = new List<GameObject>();
        onlyLargeSquareTitles = new List<GameObject>();
        textShowingOrNot = true;

        foreach (Transform child in transform)
        {
            foreach (Transform secondChild in child)
            {
                textObjects.Add(secondChild.Find("cubeLetter").gameObject);
                textObjects.Add(secondChild.Find("squareNumber").gameObject);
                textObjects.Add(secondChild.Find("layerNumber").gameObject);

                onlyLargeSquareTitles.Add(secondChild.Find("squareNumber").gameObject);

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
                foreach (GameObject thisTextObject in onlyLargeSquareTitles)
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
    public void HideText()
    {
        if (textShowingOrNot == false)
        {
            foreach(GameObject thisObject in textObjects)
            { 
                thisObject.SetActive(false);
            }
        }
        else if (textShowingOrNot == true)
        {
            foreach(GameObject thisObject in textObjects)
            { 
                thisObject.SetActive(false);
            }
            foreach(GameObject thisObject in onlyLargeSquareTitles)
            {
                thisObject.SetActive(true);
            }
        }


    }

}
