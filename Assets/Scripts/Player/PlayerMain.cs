using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain Instance;
    [SerializeField] private string playerName = "Player";
    private string currentGameState;
    private TagInputManager tagInputManager;
    private IceInputManager iceInputManager;
    private List<GameModeInputManager> inputManagers = new List<GameModeInputManager>();
    [FormerlySerializedAs("tagPlGameObj")][SerializeField] private GameObject tagVisualGameObj;
    [SerializeField] private GameObject iceVisualGameObj;   // the visuals of the player in the Ice game
    private List<GameObject> visualGamesObject = new List<GameObject>();

    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
            
        MainGameControl.OnGameStateChanged += OnGameStateChanged;
        if (TryGetComponent<IceInputManager>(out iceInputManager))
            inputManagers.Add(iceInputManager);
        else
            Debug.LogError("Error: IceInputManager Component not found!");
        if (TryGetComponent<TagInputManager>(out tagInputManager))
            inputManagers.Add(tagInputManager);
        else
            Debug.LogError("Error: TagInputManager Component not found!");
        if (tagVisualGameObj) visualGamesObject.Add(tagVisualGameObj);
        if (iceVisualGameObj) visualGamesObject.Add(iceVisualGameObj);
    }

    private void OnGameStateChanged(MainGameControl.GameStateEnum newGameState)
    {
        ChangeController(newGameState);
    }

    private void ChangeController(MainGameControl.GameStateEnum newGameState)
    {
        foreach (var im in inputManagers)
        {
            im.enabled = false;
        }

        foreach (var visual in visualGamesObject)
        {
            visual.SetActive(false);
        }
        switch (newGameState)
        {
            case MainGameControl.GameStateEnum.Lobby:
                tagInputManager.enabled = true;
                tagVisualGameObj.SetActive(true);
                Debug.Log("Controller changed to Lobby Controller");
                break;
            case MainGameControl.GameStateEnum.TagGame:
                tagInputManager.enabled = true;
                tagVisualGameObj.SetActive(true);
                Debug.Log("Controller changed to Tag Game Controller");
                break;
            case MainGameControl.GameStateEnum.IceGame:
                iceInputManager.enabled = true;
                iceVisualGameObj.SetActive(true);
                Debug.Log("Controller changed to Ice Game Controller");
                break;
            default:
                tagInputManager.enabled = true;
                break;
        }
    }
}
