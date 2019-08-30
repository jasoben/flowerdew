using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public GlobalControl controls;
    public GameObject lookRotationObject;
    public Texture2D handCursor;
    public Texture2D eyeCursor;
    private Texture2D currentCursor;
    public GlobalBool mouseLook;
    public GameObject adjustedPointer;
    
    private bool cubeTextBool;
    private bool allTextBool;

    private float distance;

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
    public float moveModifier, zoomModifier;

    //public MonoBehaviour moveMouseY;
    //public MonoBehaviour moveMouseX;

    // Use this for initialization
    void Start()
    {
        cubeTextBool = false;
        allTextBool = true;
        //moveMouseY = viewpointObject.GetComponent<MouseLookY>() as MonoBehaviour;
        //moveMouseX = mainCamera.GetComponent<MouseLookX>() as MonoBehaviour;
        mouseLook.globalBool = false;
        controls.typeOfSelect = TypeOfSelect.single;
        controls.typeOfClick = TypeOfClick.select;
        distance = GetComponent<MouseOrbit>().distance;
    }

    // LateUpdate is for camera movements (called after Update)
    
    private void LateUpdate()
    {
        Vector3 adjustedForwardDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 adjustedLateralDirection = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
        // <<Change perspective with keys
        if (Input.GetKey(moveForward) || Input.GetKey(KeyCode.W))
        {
            lookRotationObject.transform.Translate(adjustedForwardDirection * moveModifier);   //NOTE TO SELF - This is why the camera needs to be attached to a capsule or other object
        }
        else if (Input.GetKey(moveBackwards) || Input.GetKey(KeyCode.S))
        {
            lookRotationObject.transform.Translate(-adjustedForwardDirection * moveModifier);
        }
        if (Input.GetKey(moveLeft) || Input.GetKey(KeyCode.A))
        {
            lookRotationObject.transform.Translate(-adjustedLateralDirection * moveModifier);
        }
        else if (Input.GetKey(moveRight) || Input.GetKey(KeyCode.D))
        {
            lookRotationObject.transform.Translate(adjustedLateralDirection * moveModifier);
        }
        if ((Input.GetKey(flyUp)) || Input.GetKey(KeyCode.X) || (Input.GetAxis("Mouse ScrollWheel") < 0f) || zoomingOut)
        {
            if (distance < 25)
            {
                distance += zoomModifier;
                GetComponent<MouseOrbit>().distance = distance;
                ChangeSliderValue(distance);
            }
        }
        else if (Input.GetKey(flyDown) || Input.GetKey(KeyCode.Z) || (Input.GetAxis("Mouse ScrollWheel") > 0f) || zoomingIn)
        {
            if (distance > 5)
            {
                distance -= zoomModifier;
                GetComponent<MouseOrbit>().distance = distance;
                ChangeSliderValue(distance);
            }
        }


        //Mouse movement using right and middle mouse buttons

        if ((Input.GetMouseButton(1)) || (Input.GetMouseButtonDown(0) && controls.typeOfClick == TypeOfClick.view))
        {
            mouseLook.globalBool = true;
        }


        else if ((Input.GetMouseButtonUp(1)) || (Input.GetMouseButtonUp(0) && controls.typeOfClick == TypeOfClick.view))
        {
            mouseLook.globalBool = false;
            Debug.Log("ViewUp");
        }

        if ((Input.GetMouseButton(2)) || (Input.GetMouseButton(0) && controls.typeOfClick == TypeOfClick.move))
        {
            mouseLook.globalBool = false;
            float mouseY = Input.GetAxis("Mouse Y");
            float mouseX = Input.GetAxis("Mouse X");
            Vector3 mouseMoveWithMiddleButton = new Vector3(-mouseX, 0, -mouseY);
            lookRotationObject.transform.Translate(mouseMoveWithMiddleButton, adjustedPointer.transform);
        }


        if (Input.GetKey(resetView))
        {
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
        mouseLook.globalBool = false;
        controls.typeOfClick = TypeOfClick.move;
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
        currentCursor = handCursor;
    }

    public void OnEyeIconButtonPressed()
    {
        mouseLook.globalBool = false;
        controls.typeOfClick = TypeOfClick.view;
        Cursor.SetCursor(eyeCursor, Vector2.zero, CursorMode.Auto);
        currentCursor = eyeCursor;
    }

    public void OnSelectIconButtonPressed()
    {
        mouseLook.globalBool = false;
        controls.typeOfClick = TypeOfClick.select;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        currentCursor = null;
    }
        
    public void OnZoomSliderChange()
    {
        distance = GetComponent<MouseOrbit>().distance = ZoomSlider.value;
    }

    private void ChangeSliderValue(float newValue)
    {
        ZoomSlider.value = newValue;
    }
}

public enum TypeOfClick { move, view, select };
