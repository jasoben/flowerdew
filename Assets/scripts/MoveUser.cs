using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUser : MonoBehaviour
{

    public GameObject viewpointObject;
    public GameObject mainCamera;

    public string moveForward;
    public string moveBackwards;
    public string moveLeft;
    public string moveRight;
    public string flyUp;
    public string flyDown;
    public string spinCW;
    public string spinCCW;

    //public MonoBehaviour moveMouseY;
    //public MonoBehaviour moveMouseX;

    // Use this for initialization
    void Start()
    {

        viewpointObject = this.gameObject;

        //moveMouseY = viewpointObject.GetComponent<MouseLookY>() as MonoBehaviour;
        //moveMouseX = mainCamera.GetComponent<MouseLookX>() as MonoBehaviour;

       
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    // LateUpdate is for camera movements (called after Update)

    private void LateUpdate()
    {

        // <<Change perspective with keys

        if (Input.GetKey(moveForward) == true)
        {
            viewpointObject.transform.Translate(Vector3.forward / 2);   //NOTE TO SELF - This is why the camera needs to be attached to a capsule or other object
        }
        else if (Input.GetKey(moveBackwards) == true)
        {
            viewpointObject.transform.Translate(Vector3.back / 2);
        }
        else if (Input.GetKey(moveLeft) == true)
        {
            viewpointObject.transform.Translate(Vector3.left / 2);
        }
        else if (Input.GetKey(moveRight) == true)
        {
            viewpointObject.transform.Translate(Vector3.right / 2);
        }
        else if (Input.GetKey(flyUp) == true)
        {
            viewpointObject.transform.Translate(Vector3.up / 2);
        }
        else if (Input.GetKey(flyDown) == true)
        {
            viewpointObject.transform.Translate(Vector3.down / 2);
        }

        else if (Input.GetKeyDown(spinCCW) == true || Input.GetKeyDown(spinCW) == true)
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
            mainCamera.GetComponent<MouseLookX>().moveCamera = false;
        }

        

        else if (Input.GetKey(spinCCW) == true)
        {
            viewpointObject.transform.Rotate(0, -1, 0);
            viewpointObject.GetComponent<MouseLookY>().rotY = viewpointObject.transform.localRotation.eulerAngles.y;
        }
        else if (Input.GetKey(spinCW) == true)
        {
            viewpointObject.transform.Rotate(0, 1, 0);
            viewpointObject.GetComponent<MouseLookY>().rotY = viewpointObject.transform.localRotation.eulerAngles.y;
        }

        



        else if (Input.GetKeyDown("m") == true)
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = !viewpointObject.GetComponent<MouseLookY>().moveCamera;
            mainCamera.GetComponent<MouseLookX>().moveCamera = !mainCamera.GetComponent<MouseLookX>().moveCamera;
        }

    }
}
