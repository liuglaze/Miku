using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    override public void Awake()
    {
        base.Awake();
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}



//    public void LoadNewScene(GameSceneSO gameSceneSO)
//    {
//        sceneToLoad = gameSceneSO;
//        if (currentScene != null)
//        {
//            if (LoadSceneRoutine != null)
//            {
//                StopCoroutine(LoadSceneRoutine);
//            }
//            LoadSceneRoutine = StartCoroutine(nameof(LoadSceneCoroutine));
//        }
//        else
//        {
//            sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
//            currentScene = menuScene;
//        }
//    }
//}
