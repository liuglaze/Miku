using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    public GameDifficulty currentGameDifficulty;
    override public void Awake()
    {
        base.Awake();
    }

    // 适用于加载后的应用数据
    private void ApplyData()
    {
        Debug.Log("Function executed after scene load.");
        if(GameManager.Instance.loadData)
        {
            DataManager.Instance.StartApplyingData();
        }
    }

    // 加载菜单场景
    public void LoadMenuScene()
    {
        DataManager.Instance.allCollections.Clear();
        SceneManager.LoadScene(0); // 这里假设菜单场景是 Scene 0
    }

    // 加载指定难度的游戏场景
    public void LoadGameScene(GameDifficulty difficulty)
    {
        currentGameDifficulty = difficulty;
        if (GameManager.Instance.loadData)
        {
            ApplyData();
        }

        // 根据难度加载不同的游戏场景
        switch(difficulty)
        {
            case GameDifficulty.Easy:
                SceneManager.LoadScene(1); // 可以使用场景名称或者索引
                break;
            case GameDifficulty.Medium:
                SceneManager.LoadScene(2);
                break;
            case GameDifficulty.Hard:
                SceneManager.LoadScene(3);
                break;
            default:
                Debug.LogWarning("Invalid difficulty selected.");
                break;
        }
    }
}

// 游戏难度的枚举
public enum GameDifficulty
{
    Easy,
    Medium,
    Hard
}

