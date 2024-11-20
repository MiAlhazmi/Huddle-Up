using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    private void Start()
    {
        // Lock and hide the cursor
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;

        xSensitivity *= 10;
        ySensitivity *= 10;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        
        // calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        
        // apply this to our camera transform
        _cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        // rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    public Camera GetCam()
    {
        return _cam;
    }
}
