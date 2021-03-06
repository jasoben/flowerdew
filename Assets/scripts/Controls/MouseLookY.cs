﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookY : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    public bool moveCamera;

    public float rotY = 0.0f; // rotation around the up/y axis
    public float rotX = 0.0f; // rotation around the right/x axis

    public Transform lookTarget;
    private Vector3 lookTargetPoint;

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

            float mouseX = Input.GetAxis("Mouse X");
            //float mouseY = -Input.GetAxis("Mouse Y");

            //rotX += mouseY * mouseSensitivity * Time.deltaTime;
            rotY += mouseX * mouseSensitivity * Time.deltaTime;

            //rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            //Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);

            transform.RotateAround(lookTargetPoint, Vector3.back, mouseX);

            //transform.rotation = localRotation;
        }

    }
}
