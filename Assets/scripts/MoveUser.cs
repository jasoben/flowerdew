//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MoveUser : MonoBehaviour
//{

//    public GameObject viewpointObject;
//    public GameObject mainCamera;
//    private CreateMatrix matrix;
//    public GameObject matrixCubesOrigin;

//    private float distanceBetweenOriginAndPlayer;

//    public float movementModifier;

//    private bool cubeTextBool;

//    public string moveForward;
//    public string moveBackwards;
//    public string moveLeft;
//    public string moveRight;
//    public string flyUp;
//    public string flyDown;
//    public string spinCW;
//    public string spinCCW;
//    public string mouseLookMode;

//    //public MonoBehaviour moveMouseY;
//    //public MonoBehaviour moveMouseX;

//    // Use this for initialization
//    void Start()
//    {

//        viewpointObject = this.gameObject;
//        matrix = GameObject.Find("CubeDuplicator").GetComponent<CreateMatrix>();
//        cubeTextBool = false;

//        //moveMouseY = viewpointObject.GetComponent<MouseLookY>() as MonoBehaviour;
//        //moveMouseX = mainCamera.GetComponent<MouseLookX>() as MonoBehaviour;

        

//    }

//    // Update is called once per frame
//    void Update()
//    {
        

//    }

//    // LateUpdate is for camera movements (called after Update)

//    private void LateUpdate()
//    {

//        distanceBetweenOriginAndPlayer = Mathf.Clamp(-10 + viewpointObject.transform.position.y - matrixCubesOrigin.transform.position.y, 1, 10);

        
     
//        // <<Change perspective with keys

//        if (Input.GetKey(moveForward) == true)
//        {
//            viewpointObject.transform.Translate(Vector3.forward * distanceBetweenOriginAndPlayer * movementModifier);   //NOTE TO SELF - This is why the camera needs to be attached to a capsule or other object
//        }
//        else if (Input.GetKey(moveBackwards) == true)
//        {
//            viewpointObject.transform.Translate(Vector3.back * distanceBetweenOriginAndPlayer * movementModifier);
//        }
//        else if (Input.GetKey(moveLeft) == true)
//        {
//            viewpointObject.transform.Translate(Vector3.left * distanceBetweenOriginAndPlayer * movementModifier);
//        }
//        else if (Input.GetKey(moveRight) == true)
//        {
//            viewpointObject.transform.Translate(Vector3.right * distanceBetweenOriginAndPlayer * movementModifier);
//        }
//        else if ((Input.GetKey(flyUp) == true) || (Input.GetAxis("Mouse ScrollWheel") < 0f) )
//        {
//            viewpointObject.transform.Translate(Vector3.up * distanceBetweenOriginAndPlayer * movementModifier);
//        }
//        else if (Input.GetKey(flyDown) == true || (Input.GetAxis("Mouse ScrollWheel") > 0f) )
//        {
//            viewpointObject.transform.Translate(Vector3.down * distanceBetweenOriginAndPlayer * movementModifier);
//        }

//        else if (Input.GetKeyDown(spinCCW) == true || Input.GetKeyDown(spinCW) == true)
//        {
//            viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
//            mainCamera.GetComponent<MouseLookX>().moveCamera = false;
//        }

        

//        else if (Input.GetKey(spinCCW) == true)
//        {
//            viewpointObject.transform.Rotate(0, -1, 0);
//            viewpointObject.GetComponent<MouseLookY>().rotY = viewpointObject.transform.localRotation.eulerAngles.y;
//        }
//        else if (Input.GetKey(spinCW) == true)
//        {
//            viewpointObject.transform.Rotate(0, 1, 0);
//            viewpointObject.GetComponent<MouseLookY>().rotY = viewpointObject.transform.localRotation.eulerAngles.y;
//        }

//        else if (Input.GetKey(mouseLookMode) == true)
//        {
//            viewpointObject.GetComponent<MouseLookY>().moveCamera = !viewpointObject.GetComponent<MouseLookY>().moveCamera;
//            mainCamera.GetComponent<MouseLookX>().moveCamera = !mainCamera.GetComponent<MouseLookX>().moveCamera;
//        }

//        //Mouse movement using right and middle mouse buttons

//        else if (Input.GetMouseButton(2))
//            {
//                float mouseY = Input.GetAxis("Mouse Y") * distanceBetweenOriginAndPlayer * movementModifier;
//                float mouseX = Input.GetAxis("Mouse X") * distanceBetweenOriginAndPlayer * movementModifier;
//                Vector3 mouseMoveWithMiddleButton = new Vector3(-mouseX, 0, -mouseY);
//                viewpointObject.transform.Translate(mouseMoveWithMiddleButton);

//            }




//            else if (Input.GetMouseButton(1) == true)
//            {
//                viewpointObject.GetComponent<MouseLookY>().moveCamera = true;
//                mainCamera.GetComponent<MouseLookX>().moveCamera = true;
//            }


//            else if (Input.GetMouseButtonUp(1) == true)
//            {
//                viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
//                mainCamera.GetComponent<MouseLookX>().moveCamera = false;
//            }


//            else if (Input.GetKeyDown("r") == true)
//        {
//            viewpointObject.GetComponent<MouseLookY>().moveCamera = true;
//            mainCamera.GetComponent<MouseLookX>().moveCamera = true;

//            viewpointObject.GetComponent<MouseLookY>().rotY = 0f;
//            viewpointObject.GetComponent<MouseLookY>().rotX = 0f;

            
//            mainCamera.GetComponent<MouseLookX>().RotX = 0f;
//            mainCamera.GetComponent<MouseLookX>().RotY = 0f;
//        }

//        else if (Input.GetKeyDown("t") == true)
//        {
//            foreach (GameObject cubeText in matrix.cubeText)
//            {
//                cubeText.SetActive(cubeTextBool);
//            }

//            cubeTextBool = !cubeTextBool;
//        }
//    }


//}
