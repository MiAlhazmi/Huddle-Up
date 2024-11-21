using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenuController : MonoBehaviour
{
    
    [SerializeField] private Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener((() =>
        {
            Loader.LoadScene(Loader.MenuScene.MainMenuScene);
        }));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Loader.LoadScene(Loader.MenuScene.MainMenuScene);
        }
    }

    public void TagButton()
    {
        Loader.LoadScene(Loader.Scene.Lobby);
    }
}
