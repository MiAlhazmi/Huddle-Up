using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainGameControl : MonoBehaviour
{
    public static MainGameControl Instance;

    public delegate void GameStateChangedAction(GameStateEnum newGameState);
    public static event GameStateChangedAction OnGameStateChanged;
    private List<GameObject> players = new List<GameObject>();

    [SerializeField] private GameStateEnum currentGameState;
    [SerializeField] private GameStateEnum manualChooseGameStateForTest = GameStateEnum.Nothing;


    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    public enum GameStateEnum
    {
        Lobby,
        TagGame,
        IceGame,
        ControlSelection,
        Loading,     // maybe no need for this
        Nothing
    }

    public void AddPlayer(GameObject player)
    {
        players.Add(player); // Add player to the list
        
    }

    private void OnSceneChanged(Scene prev, Scene current)
    {
        switch (current.name)
        {
            case "Lobby":
                currentGameState = GameStateEnum.Lobby;
                break;
            case "TagGame":
                currentGameState = GameStateEnum.TagGame;
                break;
            case "InProgressSampleScene":
                currentGameState = GameStateEnum.IceGame;
                break;
        }
        ChangeGameState();
    }    
    private void ChangeGameState()
    {
        if (OnGameStateChanged != null)
        {
            Debug.Log("Game State Changed!");
            OnGameStateChanged(currentGameState);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (manualChooseGameStateForTest != GameStateEnum.Nothing)
        {
            currentGameState = manualChooseGameStateForTest;
            ChangeGameState();
            return;
        }
        currentGameState = GameStateEnum.Lobby;
        ChangeGameState();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Lobby");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("InProgressSampleScene");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentGameState = GameStateEnum.Lobby;
            ChangeGameState();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            currentGameState = GameStateEnum.IceGame;
            ChangeGameState();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentGameState = GameStateEnum.TagGame;
            ChangeGameState();
        }
    }
}
