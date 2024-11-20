using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float mouseSensitivity = 0.5f;
    private float _cameraVerticalRotation = 0f;
    private Vector2 look = Vector2.zero;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0.7f, 0.3f);
    
    // for the old input system
    private float inputX;
    private float inputY;
    
    // Start is called before the first frame update
    void Start()
    {
        // Lock and hide the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Take input from the user:
    // inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
    // inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;
    
        // look *= mouseSensitivity;
        
        // Rotate the camera around its local x axis
    // _cameraVerticalRotation -= inputY;
        _cameraVerticalRotation -= look.y;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 70f); // restrict the rotation between the given value
        // transform.localEulerAngles = Vector3.right * _cameraVerticalRotation; // Vector3.right == new Vector3(1, 0, 0)
        // transform.eulerAngles = Vector3.up * look.x;
        // Rotate the player object and the camera around their y axis
        player.Rotate(Vector3.up * look.x); // Vector3.up == new Vector3(0, 1, 0)
        transform.Rotate(Vector3.up * look.x);
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = player.position + cameraOffset;
    }
}
