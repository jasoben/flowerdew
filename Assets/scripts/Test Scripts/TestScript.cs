using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public GameObject thisThing;

	// Use this for initialization
	void Start () {
        for (int x = 0; x < 10; x++)
        {
            GameObject newThing = Instantiate(thisThing, transform.position + new Vector3(x, 0, 0), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
