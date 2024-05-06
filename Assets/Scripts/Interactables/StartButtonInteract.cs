using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonInteract : Interactable
{
    [SerializeField] private GameControl _gameControl;
    // Start is called before the first frame update

    private void Awake()
    {
        _gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
    }

    void Start()
    {
        promptMessage = "Click to start the game";
    }

    protected override void Interact(GameObject playerGameObj)
    {
        _gameControl.OnStartButtonClicked();
    }
}
