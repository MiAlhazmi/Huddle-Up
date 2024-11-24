using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.GameControl;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
    private GameObject _player;  // The player who paused.
    private bool _isPaused = false
        ;
    void Start()
    {
        PauseGameController.Instance.OnGamePaused += PauseGameController_OnGamePaused;
        PauseGameController.Instance.OnGameUnpaused += PauseGameController_OnGameUnpaused;
        Hide();
    }


    private void PauseGameController_OnGameUnpaused(object sender, GamePauseEventArgs e)
    {
        _isPaused = e.isPaused;
        if (_player.Equals(e.playerGameObject))
            Unpause();
        else
            Debug.Log("Another player is paused, You cannot unpause.");
    }

    private void PauseGameController_OnGamePaused(object sender, GamePauseEventArgs e)
    {
        _isPaused = e.isPaused;
        _player = e.playerGameObject;
        Pause();
    }

    private void Pause()
    {
        Show();
    }

    private void Unpause()
    {
        Hide();
    }
    
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
