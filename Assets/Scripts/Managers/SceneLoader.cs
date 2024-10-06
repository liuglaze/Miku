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
    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // 确保在对象销毁时移除事件监听器，避免内存泄漏
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // 当场景加载完成后触发的回调函数
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Debug.Log("Game Scene Loaded! Executing function.");
            if(GameManager.Instance.loadData)
            {
                ApplyData();
            }
            else
            {
                DataManager.Instance.allCollections.Clear();
            }
        }
    }

    private void ApplyData()
    {
        // 执行的函数
        Debug.Log("Function executed after scene load.");
        if(GameManager.Instance.loadData)
        {
            DataManager.Instance.StartApplyingData();
        }
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameScene()
    {
        if(GameManager.Instance.loadData)
            {
                ApplyData();
            }
            else
            {
                DataManager.Instance.allCollections.Clear();
            }
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
