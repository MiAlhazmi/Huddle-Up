using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.GameControl;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    public static PauseGameController Instance { get; private set; } // to access HandlePauseRequest()
    public event EventHandler<GamePauseEventArgs> OnGamePaused;
    public event EventHandler<GamePauseEventArgs> OnGameUnpaused;
    
    private bool _isGamePaused = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    
    public bool HandlePauseRequest(GameObject player) // player: is the gameobject of the player who requested to pause
    {
        if (player == null)
        {
            Debug.LogError("Player is null");
            return false;
        }
        _isGamePaused = !_isGamePaused;
        Debug.Log("Player: " + player.name + _isGamePaused);
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
