using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenuController : MonoBehaviour
{
    
    [SerializeField] private Button tagButton;

    private void Awake()
    {
        tagButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.Lobby);
        });
    }
}
