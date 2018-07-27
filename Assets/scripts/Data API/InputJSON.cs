using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJSON : MonoBehaviour {

    private string JSONData;
    private bool showData;

	// Use this for initialization
	void Start () {
        showData = false;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ReceiveData(string thisData)
    {
        JSONData = thisData;
        showData = true;
    }

    private void OnGUI()
    {
        if (showData)
            GUI.Label(new Rect(Screen.width - 200, 0, 200, Screen.height - 40), JSONData);
    }
}
