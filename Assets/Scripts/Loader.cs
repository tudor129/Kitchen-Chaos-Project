using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    
    static Scene _targetScene;

    public static void Load(Scene targetScene)
    {
        Loader._targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        
        
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(_targetScene.ToString());
    }
    
    
}
