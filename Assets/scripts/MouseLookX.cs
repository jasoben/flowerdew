using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookX : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    public GameObject viewpointUser;

    Quaternion viewpointRotation;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseY * mouseSensitivity * Time.deltaTime;
        //rotY += mouseX * mouseSensitivity * Time.deltaTime;

        //rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        viewpointRotation = viewpointUser.transform.rotation;
                
        Quaternion localRotation = Quaternion.Euler(rotX, viewpointRotation.eulerAngles.y, 0);
        transform.rotation = localRotation;
    }
}