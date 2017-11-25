using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    public GameObject viewpointObject;
    public GameObject mainCamera;
    private CubeBuffer cubeBuffer;
    public GameObject masterObject;

    private float distanceBetweenOriginAndPlayer;

    public float movementModifier;

    private bool cubeTextBool;

    public KeyCode moveForward;
    public KeyCode moveBackwards;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode flyUp;
    public KeyCode flyDown;
    public KeyCode spinCW;
    public KeyCode spinCCW;
    public KeyCode mouseLookMode;
    public KeyCode layerUp;
    public KeyCode layerDown;
    public KeyCode mapOnOff;
    public KeyCode modelOnOff;
    public KeyCode resetView;
    public KeyCode textOnOff;


    //public MonoBehaviour moveMouseY;
    //public MonoBehaviour moveMouseX;

    // Use this for initialization
    void Start()
    {

        viewpointObject = this.gameObject;
        masterObject = GameObject.Find("MasterObject");
        cubeBuffer = masterObject.GetComponent<CubeBuffer>();
        cubeTextBool = false;

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

        distanceBetweenOriginAndPlayer = Mathf.Clamp(-10 + viewpointObject.transform.position.y - masterObject.transform.position.y, 1, 10);



        // <<Change perspective with keys

        if (Input.GetKey(moveForward))
        {
            viewpointObject.transform.Translate(Vector3.forward * distanceBetweenOriginAndPlayer * movementModifier);   //NOTE TO SELF - This is why the camera needs to be attached to a capsule or other object
        }
        else if (Input.GetKey(moveBackwards))
        {
            viewpointObject.transform.Translate(Vector3.back * distanceBetweenOriginAndPlayer * movementModifier);
        }
        if (Input.GetKey(moveLeft))
        {
            viewpointObject.transform.Translate(Vector3.left * distanceBetweenOriginAndPlayer * movementModifier);
        }
        else if (Input.GetKey(moveRight))
        {
            viewpointObject.transform.Translate(Vector3.right * distanceBetweenOriginAndPlayer * movementModifier);
        }
        if ((Input.GetKey(flyUp)) || (Input.GetAxis("Mouse ScrollWheel") < 0f))
        {
            viewpointObject.transform.Translate(Vector3.up * distanceBetweenOriginAndPlayer * movementModifier);
        }
        else if (Input.GetKey(flyDown) || (Input.GetAxis("Mouse ScrollWheel") > 0f))
        {
            viewpointObject.transform.Translate(Vector3.down * distanceBetweenOriginAndPlayer * movementModifier);
        }

        if (Input.GetKeyDown(spinCCW) || Input.GetKeyDown(spinCW))
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
            mainCamera.GetComponent<MouseLookX>().moveCamera = false;
        }



        if (Input.GetKey(spinCCW))
        {
            viewpointObject.transform.Rotate(0, -1, 0);
            viewpointObject.GetComponent<MouseLookY>().rotY = viewpointObject.transform.localRotation.eulerAngles.y;
        }
        else if (Input.GetKey(spinCW))
        {
            viewpointObject.transform.Rotate(0, 1, 0);
            viewpointObject.GetComponent<MouseLookY>().rotY = viewpointObject.transform.localRotation.eulerAngles.y;
        }

        if (Input.GetKeyDown(mouseLookMode))
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = !viewpointObject.GetComponent<MouseLookY>().moveCamera;
            mainCamera.GetComponent<MouseLookX>().moveCamera = !mainCamera.GetComponent<MouseLookX>().moveCamera;
        }

        //Mouse movement using right and middle mouse buttons

        if (Input.GetMouseButton(2))
        {
            float mouseY = Input.GetAxis("Mouse Y") * distanceBetweenOriginAndPlayer * movementModifier;
            float mouseX = Input.GetAxis("Mouse X") * distanceBetweenOriginAndPlayer * movementModifier;
            Vector3 mouseMoveWithMiddleButton = new Vector3(-mouseX, 0, -mouseY);
            viewpointObject.transform.Translate(mouseMoveWithMiddleButton);

        }




        if (Input.GetMouseButton(1))
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = true;
            mainCamera.GetComponent<MouseLookX>().moveCamera = true;
        }


        else if (Input.GetMouseButtonUp(1))
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
            mainCamera.GetComponent<MouseLookX>().moveCamera = false;
        }


        if (Input.GetKey(resetView))
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = true;
            mainCamera.GetComponent<MouseLookX>().moveCamera = true;

            viewpointObject.GetComponent<MouseLookY>().rotY = 0f;
            viewpointObject.GetComponent<MouseLookY>().rotX = 0f;


            mainCamera.GetComponent<MouseLookX>().RotX = 0f;
            mainCamera.GetComponent<MouseLookX>().RotY = 0f;
        }

        if (Input.GetKeyDown(textOnOff))
        {
            foreach (GameObject cubeText in cubeBuffer.CubeText)
            {
                cubeText.SetActive(cubeTextBool);
            }

            cubeTextBool = !cubeTextBool;
        }

        if (Input.GetKeyDown(layerDown))
        {
            LayerNavigator.ActivateOrDeactivateLayer(LayerNavigator.CurrentLayer, false);
            LayerNavigator.ChangeLayerTo(LayerNavigator.CurrentLayer + 1);
            LayerNavigator.ActivateOrDeactivateLayer(LayerNavigator.CurrentLayer, true);
        }
        else if (Input.GetKeyDown(layerUp))
        {
            LayerNavigator.ActivateOrDeactivateLayer(LayerNavigator.CurrentLayer, true);
            LayerNavigator.ChangeLayerTo(LayerNavigator.CurrentLayer - 1);
            LayerNavigator.ActivateOrDeactivateLayer(LayerNavigator.CurrentLayer, true);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            CubeBuffer.ClearBuffer();
        }
    }


}
