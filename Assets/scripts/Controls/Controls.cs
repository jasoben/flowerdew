using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public GameObject viewpointObject;
    public GameObject mainCamera;
    private CubeBuffer cubeBuffer;
    public GameObject masterObject;
    public Texture2D handCursor;
    public Texture2D eyeCursor;
    public Texture2D currentCursor;
    
    private float distanceBetweenOriginAndPlayerClamped;
    private float distanceBetweenOriginAndPlayer;
    private Vector3 originalCameraLocation;

    public float movementModifier;

    private bool cubeTextBool;
    private bool allTextBool;

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

    public Button ZoomIn, ZoomOut, HandIcon, EyeIcon;
    public Slider ZoomSlider;
    private bool zoomingIn, zoomingOut;

    public TypeOfClick thisClick;

    //public MonoBehaviour moveMouseY;
    //public MonoBehaviour moveMouseX;

    // Use this for initialization
    void Start()
    {

        viewpointObject = this.gameObject;
        masterObject = GameObject.Find("MasterObject");
        cubeBuffer = masterObject.GetComponent<CubeBuffer>();
        cubeTextBool = false;
        allTextBool = true;
        //moveMouseY = viewpointObject.GetComponent<MouseLookY>() as MonoBehaviour;
        //moveMouseX = mainCamera.GetComponent<MouseLookX>() as MonoBehaviour;
        thisClick = TypeOfClick.select;
        originalCameraLocation = viewpointObject.transform.position;       

    }

    // Update is called once per frame
    void Update()
    {

    }

    // LateUpdate is for camera movements (called after Update)
    
    private void LateUpdate()
    {

        distanceBetweenOriginAndPlayer = viewpointObject.transform.position.y - masterObject.transform.position.y;
        distanceBetweenOriginAndPlayerClamped = Mathf.Clamp(-10 + viewpointObject.transform.position.y - masterObject.transform.position.y, 1, 10);

        // <<Change perspective with keys
        if (Input.GetKey(moveForward) || Input.GetKey(KeyCode.W))
        {
            viewpointObject.transform.Translate(Vector3.forward * distanceBetweenOriginAndPlayerClamped * movementModifier);   //NOTE TO SELF - This is why the camera needs to be attached to a capsule or other object
        }
        else if (Input.GetKey(moveBackwards) || Input.GetKey(KeyCode.S))
        {
            viewpointObject.transform.Translate(Vector3.back * distanceBetweenOriginAndPlayerClamped * movementModifier);
        }
        if (Input.GetKey(moveLeft) || Input.GetKey(KeyCode.A))
        {
            viewpointObject.transform.Translate(Vector3.left * distanceBetweenOriginAndPlayerClamped * movementModifier);
        }
        else if (Input.GetKey(moveRight) || Input.GetKey(KeyCode.D))
        {
            viewpointObject.transform.Translate(Vector3.right * distanceBetweenOriginAndPlayerClamped * movementModifier);
        }
        if ((Input.GetKey(flyUp)) || Input.GetKey(KeyCode.X) || (Input.GetAxis("Mouse ScrollWheel") < 0f) || zoomingOut)
        {
            Debug.Log(distanceBetweenOriginAndPlayer);
            if (distanceBetweenOriginAndPlayer < 25)
            {
                viewpointObject.transform.Translate(Vector3.up * distanceBetweenOriginAndPlayerClamped * movementModifier);
                ChangeSliderValue(viewpointObject.transform.position.y);
            }
        }
        else if (Input.GetKey(flyDown) || Input.GetKey(KeyCode.Z) || (Input.GetAxis("Mouse ScrollWheel") > 0f) || zoomingIn)
        {
            Debug.Log(distanceBetweenOriginAndPlayer);
            if (distanceBetweenOriginAndPlayer > 1)
            {
                viewpointObject.transform.Translate(Vector3.down * distanceBetweenOriginAndPlayerClamped * movementModifier);
                ChangeSliderValue(viewpointObject.transform.position.y);
            }
        }

        if (Input.GetKeyDown(spinCCW) || Input.GetKeyDown(spinCW) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
            mainCamera.GetComponent<MouseLookX>().moveCamera = false;
        }



        if (Input.GetKey(spinCCW) || Input.GetKey(KeyCode.Q))
        {
            viewpointObject.transform.Rotate(0, -1, 0);
            viewpointObject.GetComponent<MouseLookY>().rotY = viewpointObject.transform.localRotation.eulerAngles.y;
        }
        else if (Input.GetKey(spinCW) || Input.GetKey(KeyCode.E))
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

        if ((Input.GetMouseButton(1)) || (Input.GetMouseButton(0) && thisClick == TypeOfClick.view))
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = true;
            mainCamera.GetComponent<MouseLookX>().moveCamera = true;
        }


        else if ((Input.GetMouseButtonUp(1)) || (Input.GetMouseButtonUp(0) && thisClick == TypeOfClick.view))
        {
            viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
            mainCamera.GetComponent<MouseLookX>().moveCamera = false;
        }

        if ((Input.GetMouseButton(2)) || (Input.GetMouseButton(0) && thisClick == TypeOfClick.move))
        {
            float mouseY = Input.GetAxis("Mouse Y") * distanceBetweenOriginAndPlayerClamped * movementModifier;
            float mouseX = Input.GetAxis("Mouse X") * distanceBetweenOriginAndPlayerClamped * movementModifier;
            Vector3 mouseMoveWithMiddleButton = new Vector3(-mouseX, 0, -mouseY);
            viewpointObject.transform.Translate(mouseMoveWithMiddleButton);

        }


        if (Input.GetKey(resetView))
        {
            viewpointObject.transform.position = originalCameraLocation;
            viewpointObject.GetComponent<MouseLookY>().moveCamera = true;
            mainCamera.GetComponent<MouseLookX>().moveCamera = true;

            viewpointObject.GetComponent<MouseLookY>().rotY = 0f;
            viewpointObject.GetComponent<MouseLookY>().rotX = 0f;

            mainCamera.GetComponent<MouseLookX>().RotX = 90f;
            mainCamera.GetComponent<MouseLookX>().RotY = 0f;
        }

        if (Input.GetKeyDown(textOnOff))
        {
            allTextBool = !allTextBool;
            GameObject.Find("TextToggle").GetComponent<Toggle>().isOn = allTextBool;
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

    public void UITextToggle()
    {
        CubeBuffer.TextVisibilitySwitcher();
    }

    public void OnZoomOutButtonPressed()
    {
        zoomingOut = true;
    }

    public void OnZoomInButtonPressed()
    {
        zoomingIn = true;
    }

    public void OnZoomOutButtonReleased()
    {
        zoomingOut = false;
    }

    public void OnZoomInButtonReleased()
    {
        zoomingIn = false;
    }

    public void OnButtonHover()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void OnButtonHoverExit()
    {
        Cursor.SetCursor(currentCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnHandIconButtonPressed()
    {
        thisClick = TypeOfClick.move;
        viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
        mainCamera.GetComponent<MouseLookX>().moveCamera = false;
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
        currentCursor = handCursor;
    }

    public void OnEyeIconButtonPressed()
    {
        thisClick = TypeOfClick.view;
        Cursor.SetCursor(eyeCursor, Vector2.zero, CursorMode.Auto);
        currentCursor = eyeCursor;
    }

    public void OnSelectIconButtonPressed()
    {
        thisClick = TypeOfClick.select;
        viewpointObject.GetComponent<MouseLookY>().moveCamera = false;
        mainCamera.GetComponent<MouseLookX>().moveCamera = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        currentCursor = null;
    }
        
    public void OnZoomSliderChange()
    {
        viewpointObject.transform.position = new Vector3(viewpointObject.transform.position.x, ZoomSlider.value, viewpointObject.transform.position.z);
    }

    private void ChangeSliderValue(float newValue)
    {
        ZoomSlider.value = newValue;
    }
}

public enum TypeOfClick { move, view, select };
