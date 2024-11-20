using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScoreboardUI : MonoBehaviour
{
    [SerializeField] private TextMesh pl1ScoreTxt;
    [SerializeField] private TextMesh pl2ScoreTxt;
    [SerializeField] private TextMesh pl3ScoreTxt;
    [SerializeField] private TextMesh pl4ScoreTxt;
    [SerializeField] private GameControl _gameControl; 
    private List<TextMesh> playersScoreTxt = new List<TextMesh>();


    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        if(!next.Equals(SceneManager.GetSceneByName("Lobby"))) return;
        if (_gameControl.IsUnityNull()) _gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
        UpdateScore();
    }

    private void Awake()
    {
        _gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
        playersScoreTxt.Add(pl1ScoreTxt);
        playersScoreTxt.Add(pl2ScoreTxt);
        playersScoreTxt.Add(pl3ScoreTxt);
        playersScoreTxt.Add(pl4ScoreTxt);
        // UpdateScore();
    }
    
    private void Start()
    {
        
    }

    public void UpdateScore()
    {
        for (int i = 0; i < _gameControl._playersList.Count; i++)
        {
            if (playersScoreTxt[i] != null)
                playersScoreTxt[i].text = _gameControl.scoreboard[_gameControl._playersList[i].GetInstanceID()].ToString();
        }
    }
}
