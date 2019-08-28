using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookX : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;
    public float clampAngle = 80.0f;
    public Transform lookTarget;
    private Vector3 lookTargetPoint;

    public bool moveCamera;

    public GameObject viewpointUser;

    Quaternion viewpointRotation;

    public float rotY = 0.0f; // rotation around the up/y axis
    public float rotX = 0.0f; // rotation around the right/x axis

    public float RotY
    {
        get { return rotY; }
        set { rotY = value; }
    }

    public float RotX
    {
        get { return rotX; }
        set { rotX = value; }
    }

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        moveCamera = false;
        lookTargetPoint = lookTarget.position;
    }

    void Update()
    {
        if (moveCamera)
        {

            //float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotX += mouseY * mouseSensitivity * Time.deltaTime;
            //rotY += mouseX * mouseSensitivity * Time.deltaTime;

            //rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            viewpointRotation = viewpointUser.transform.rotation;

//            Quaternion localRotation = Quaternion.Euler(rotX, viewpointRotation.eulerAngles.y, 0);

            transform.RotateAround(lookTargetPoint, Vector3.left, mouseY);
//            transform.rotation = localRotation;
        }
    }

   
}