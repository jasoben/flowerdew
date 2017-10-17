using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTextEnabler : MonoBehaviour {

    //GameObjects
    private List<GameObject> textObjects;
    public List<GameObject> TextObjects
    {
        get { return textObjects; }        
    }
	// Use this for initialization
	void Start () {

        textObjects = new List<GameObject>();

        foreach (Transform child in transform)
        {
            foreach (Transform secondChild in child)
            {
                textObjects.Add(secondChild.transform.Find("cubeLetter").transform.gameObject);
                textObjects.Add(secondChild.transform.Find("squareNumber").transform.gameObject);
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
