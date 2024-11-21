using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Lobby,
        Playground,
        Camp,
        Village,
        LoadingScene
    }
    
    public enum MenuScene
    {
        MainMenuScene,
        GameModesScene,
        StatsScene
    }

    public static Scene targetScene;

    public static void LoadScene(Scene scene)
    {
        Loader.targetScene = scene;
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
        SceneManager.LoadScene(targetScene.ToString());
    }
}
