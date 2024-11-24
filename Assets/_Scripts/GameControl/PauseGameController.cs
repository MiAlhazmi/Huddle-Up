using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.GameControl;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    // public class GamePauseEventArgs : EventArgs
    // {
    //     private GameObject _playerGameObject;
    //     private bool _isPaused;
    //     public GamePauseEventArgs(GameObject player, bool isPaused){
    //         this._playerGameObject = player;
    //         this._isPaused = isPaused;
    //     }
    // }

    private bool _isGamePaused = false;
    public event EventHandler<GamePauseEventArgs> OnGamePaused;
    public event EventHandler<GamePauseEventArgs> OnGameUnpaused;
    
    public static PauseGameController Instance { get; private set; } // to access HandlePauseRequest()

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private bool HandlePauseRequest(GameObject player) // the gameobject of the player who requested to pause
    {
        if (player == null)
        {
            Debug.LogError("Player is null");
            return false;
        }
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused)
        {
            OnGamePaused?.Invoke(this, new GamePauseEventArgs(player, _isGamePaused));
            Time.timeScale = 0f;
        }        
        else
        {
            OnGameUnpaused?.Invoke(this, new GamePauseEventArgs(player, _isGamePaused));
            Time.timeScale = 1f;
        }
        return true;
    }
}
