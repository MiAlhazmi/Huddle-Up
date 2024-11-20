using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonInteract : Interactable
{
    [SerializeField] private GameControl _gameControl;

    [SerializeField] private SoundEffectsControl _sECtrl;
    // Start is called before the first frame update

    private void Awake()
    {
        _gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
        _sECtrl = _gameControl.GetComponent<SoundEffectsControl>();
    }

    void Start()
    {
        promptMessage = "Click to start the game";
    }

    protected override void Interact(GameObject playerGameObj)
    {
        _sECtrl.PlayButtonClick();
        _gameControl.OnStartButtonClicked();
    }
}
