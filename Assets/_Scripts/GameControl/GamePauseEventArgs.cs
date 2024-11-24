using System;
using UnityEngine;

namespace _Scripts.GameControl
{
    public class GamePauseEventArgs : EventArgs
    {
        public GameObject playerGameObject { get; private set; }
        public bool isPaused { get; private set; }

        public GamePauseEventArgs(GameObject player, bool isPaused){
            this.playerGameObject = player;
            this.isPaused = isPaused;
        }
    }
}