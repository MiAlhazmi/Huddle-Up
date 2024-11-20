using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGame : MonoBehaviour
{
    // // StartTimer()
    // public void StartGameTimer()
    // {
    //     startGameTimerObj.SetActive(true);
    //     timer.StartTimerNumber(1); // this starts the GameStartingTimer
    // }
    // // StartGame() -> start the timer, unfreeze, call giveTagRandome()
    // public void StartGame()
    // {
    //     startGameTimerObj.SetActive(false);
    //     gameTimerObj.SetActive(true);
    //     GiveRandomPos();
    //     GiveTagRandom();
    //     timer.StartTimerNumber(0); // this starts the game timer
    //     
    // }
    // // EndGame()
    // public void EndGame()
    // {
    //     canvasGameEnd.SetActive(true);
    //     Time.timeScale = 0;
    // }
    //
    // private void GiveRandomPos()
    // {
    //     Debug.Log("player list count: " + _playersList.Count);
    //     if (_playersList.Count <= 0) return;
    //     foreach (var player in _playersList)
    //     {
    //         player.GetComponent<CharacterController>().enabled = false;     // because CharacterController component won't allow to change the position 
    //         float xPos = Random.Range(minValueX, maxValueX);
    //         float zPos = Random.Range(minValueZ, maxValueZ);
    //         Vector3 pos = new Vector3(xPos, 10f, zPos);
    //         player.transform.position = new Vector3(xPos, 10f, zPos);
    //         player.GetComponent<CharacterController>().enabled = true;
    //     }
    //     
    // }
    // public void GiveTagRandom()
    // {
    //     if (currentTagOwner != null) return;
    //     if (_playersList.Count <= 0) return;
    //     
    //     int randomNumber = Random.Range(0, _playersList.Count);
    //     _playersList[randomNumber].GetComponent<Player>().SetIsTagger(true);
    //     SetTagOwner(_playersList[randomNumber]);
    //     Debug.Log("Tag was given randomly!");
    // }
}
