using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position;
        Vector3 newForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.right);
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

    }
}
