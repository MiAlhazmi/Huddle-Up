using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonInteract : Interactable
{
    [SerializeField] private GameControl _gameControl;

    private void Awake()
    {
        _gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();

    }

    // Start is called before the first frame update
    void Start()
    {
        promptMessage = "Click to go back to Main Menu";
    }

    protected override void Interact(GameObject playerGameObj)
    {
        _gameControl.QuitGame(true);
    }
}
