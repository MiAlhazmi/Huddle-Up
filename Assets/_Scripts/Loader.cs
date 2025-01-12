using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MenuScene,
        Lobby,
        Playground,
        Camp,
        Village,
        LoadingScene
    }
    
    public enum MenuScene
    {
        MenuScene,
        GameModesScene,
        StatsScene
    }

    private static Scene _targetScene;

    // Use this if you want to have a loading scene between scense 
    public static void LoadScene(Scene scene)
    {
        Loader._targetScene = scene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    
    // No need for the loading scene when navigating through menus scenes.
    public static void LoadScene(MenuScene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    // This function is a callback from the loading scene, just to make sure the loading scene is shown before
    // going to the next one.
    public static void LoaderCallback()
    {
        SceneManager.LoadScene(_targetScene.ToString());
    }
}
