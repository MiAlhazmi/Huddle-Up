using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorInteract : Interactable
{
    // [SerializeField] private Color color;
    [SerializeField] private MeshRenderer _meshRenderer;
    private Color _thisColor;
    [SerializeField] private String colorName;
    private bool _locked;
    private GameObject _colorOwner; // this points to the player who took the color
    
    // Start is called before the first frame update
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _thisColor = _meshRenderer.material.color;
        _locked = false;
        promptMessage = "Pick " + colorName;
    }

    protected override void Interact(GameObject playerGameObj)
    {
        if (!_locked || _colorOwner.GetComponent<Player>().GetPlayerColor() != _thisColor)
        {
            playerGameObj.GetComponent<Player>().ChangePlayerColor(_thisColor);
            _colorOwner = playerGameObj;
            _locked = true;
            playerGameObj.GetComponent<PlayerUI>().UpdateNotificationText("You're now " + colorName);
        } else if (IsOwner(playerGameObj))
        {
            playerGameObj.GetComponent<PlayerUI>().UpdateNotificationText("You're " + colorName);
        }
        else
        {
            playerGameObj.GetComponent<PlayerUI>().UpdateNotificationText(colorName + " is taken");
        }
    }

    private bool IsOwner(GameObject playerGameObj)
    {
        return _colorOwner == playerGameObj;
    }
}
