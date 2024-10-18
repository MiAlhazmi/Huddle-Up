using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActionsDefault _playerInputActions;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActionsDefault();
        _playerInputActions.Player.Enable(); // Player is the action map that I created in the input actions not the gameobject
    }

    public Vector2 GameMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public bool GameMovementJump()
    {
        float jumpInput = _playerInputActions.Player.Jump.ReadValue<float>();
        Debug.Log("Jump.ReadValue<float>() = " + jumpInput);
        return jumpInput > 0;
        
        //return _playerInputActions.Player.Jump.ReadValue<float>();
    }
    
}
