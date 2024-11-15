using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceInputManager : GameModeInputManager
{
    private PlayerInputActionsDefault playerInputActions;
    private PlayerInputActionsDefault.IceGameModeActions iceGameModeActions;
    private PlayerInput playerInput;
    private IcePlMovement plMovement;
    
    private Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        playerInputActions = new PlayerInputActionsDefault();
        iceGameModeActions = playerInputActions.IceGameMode;
        playerInput = GetComponent<PlayerInput>();
        plMovement = GetComponentInChildren<IcePlMovement>(true);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // pass movement input to PlayerMovement component
        inputVector = context.ReadValue<Vector2>();
        Debug.Log("OnMove Action occured!");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            plMovement.Jump();
            Debug.Log("OnJump Action occured!");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        plMovement.ProcessMove(inputVector);
    }
    private void OnEnable()
    {
        iceGameModeActions.Enable();
        playerInput.SwitchCurrentActionMap("IceGameMode");
    }

    private void OnDisable()
    {
        iceGameModeActions.Disable();
    }
}
