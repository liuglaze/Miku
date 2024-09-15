using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int deathCount = 0; // 玩家死亡次数
    public float completionTime; // 通关时间
    public Transform player;
    private void Awake()
    {
        // 确保只有一个GameManager实例存在（单例模式）
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        completionTime += Time.deltaTime;
    }

    // 获取已收集道具的数量
    public int GetCollectedItems()
    {
        return RewardManager.Instance.strawBerryAmount;
    }

    // 获取玩家死亡次数
    public int GetDeathCount()
    {
        return deathCount;
    }

    // 获取通关时间
    public float GetCompletionTime()
    {
        return completionTime;
    }
}

