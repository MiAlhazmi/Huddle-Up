using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayerControl : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip plGrMusic;
    [SerializeField] private AudioClip campMusic;
    [SerializeField] private AudioClip villageMusic;
    [SerializeField] private AudioClip music3;

    private MapSelectionControl _mapSelectionControl;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }
    
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
    
    private void Awake()
    {
        _mapSelectionControl = GetComponent<MapSelectionControl>();
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        Invoke(nameof(PlayMapMusic), 4.5f); // loading time
    }
    
    public void PlayMapMusic()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            StopMusic();
            return;
        }
        switch (_mapSelectionControl.selectedMap)
        {
            case "Playground":
                Debug.Log("PLayground music plays");
                PlayPlGrMusic();
                break;
            case "Camp":
                PlayCampMusic();
                break;
            case "Village":
                PlayVillageMusic();
                break;
            default:
                StopMusic();
                break;
        }
    }
    
    private void PlayPlGrMusic()
    {
        if (src == null) return;
        src.clip = plGrMusic;
        src.Play();
    }
    
    private void PlayCampMusic()
    {
        if (src == null) return;
        src.clip = campMusic;
        src.Play();
    }
    
    private void PlayVillageMusic()
    {
        if (src == null) return;
        src.clip = villageMusic;
        src.Play();
    }
    
    public void StopMusic()
    {
        if (src == null) return;
        src.Stop();
    }
}
