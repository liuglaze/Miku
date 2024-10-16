using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
[Serializable]
public class SerializeVector3
{
    public float x; public float y; public float z;
    public SerializeVector3(Vector3 vector)
    {
        x = vector.x; y = vector.y; z = vector.z;
    }
    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}
[Serializable]
public class ContinueData
{
    public GameDifficulty currentDifficulty;
    public SerializeVector3 currentSavePos;
    public int deathCount;
    public float completeTime;
    public int collectionAmount;
    public ContinueData(GameDifficulty gameDifficulty,Vector3 currentSavePos,int deathCount,float completeTime,int collectionAmount)
    {
        this.currentDifficulty = gameDifficulty;
        this.currentSavePos = new SerializeVector3(currentSavePos);
        this.deathCount = deathCount;
        this.completeTime = completeTime;
        this.collectionAmount = collectionAmount;
    }
}
[Serializable]
public class RankingListData
{
    public float completeTime;
    public int collectionAmount;
    public int deathCount;
    public string playerID;
    public RankingListData(int deathCount, float completeTime, int collectionAmount, string playerID)
    {
        this.deathCount = deathCount;
        this.completeTime = completeTime;
        this.collectionAmount = collectionAmount;
        this.playerID = playerID;
    }
}
[Serializable]
public class CollectionData
{
    public string guid;
    public bool hasUsed;
}

public class DataManager : Singleton<DataManager>
{
    public ContinueData currentContinueData;
    public List<RankingListData> ranksData;
    public List<CollectionData> collectionData = new List<CollectionData>();
    public List<CollectionGuid> allCollections = new List<CollectionGuid>(); // 用于存储所有注册的 CollectionGuid
    private string continueDataFilePath;
    private string rankingFilePath;
    private string collectionFilePath;

    // 自定义路径，例如 D:\unity\Miku\Assets\Data
    private string customDataDirectory = @"D:\unity\Miku\Assets\Data";

    override public void Awake()
    {
        base.Awake();
        // 确保目录存在，如果不存在则创建
        if (!Directory.Exists(customDataDirectory))
        {
            Directory.CreateDirectory(customDataDirectory);
        }
        // 设置 ContinueData 和 Ranking和CollectionData 的路径
        continueDataFilePath = Path.Combine(customDataDirectory, "continueData.json");
        rankingFilePath = Path.Combine(customDataDirectory, "ranking.json");
        collectionFilePath = Path.Combine(customDataDirectory, "collection.json");
        currentContinueData = LoadGame();
        ranksData = LoadRanking();
        collectionData = LoadCollectionData();
        DontDestroyOnLoad(this);
    }

    // 注册 CollectionGuid
    public void RegisterCollection(CollectionGuid collection)
    {
        if (!allCollections.Contains(collection))
        {
            allCollections.Add(collection);
        }
        
    }

    // 保存所有 CollectionGuid 的数据
    public void SaveCollections()
    {
        // 先清除所有 null 元素
        allCollections.RemoveAll(item => item == null);

        collectionData.Clear();  // 清空之前的数据

        // 遍历所有注册的 CollectionGuid 对象
        foreach (var collection in allCollections)
        {
            if (collection != null) // 确保不是 null
            {
                collectionData.Add(new CollectionData
                {
                    guid = collection.GetGuid(),
                    hasUsed = collection.hasCollect
                });
            }
        }

        // 将 collectionData 列表保存为 JSON 文件
        string json = JsonUtility.ToJson(new CollectionDataWrapper(collectionData));
        File.WriteAllText(collectionFilePath, json);
    }

    // 反向加载 CollectionData
    public List<CollectionData> LoadCollectionData()
    {
        if (File.Exists(collectionFilePath))
        {
            string json = File.ReadAllText(collectionFilePath);
            CollectionDataWrapper wrapper = JsonUtility.FromJson<CollectionDataWrapper>(json);
            return wrapper.collectionData;
        }
        return new List<CollectionData>(); // 如果文件不存在，返回空列表
    }
    // 根据 GUID 查找 hasCollect 状态
    public bool GetHasCollectedStatus(string guid)
    {
        // 遍历已加载的 collectionData 列表，查找匹配的 GUID
        foreach (var data in collectionData)
        {
            if (data.guid == guid)
            {
                return data.hasUsed;
            }
        }
        return false; // 默认返回 false，表示没有找到已收集状态
    }
    // 保存 ContinueData
    public void SaveGame(ContinueData continueData)
    {
        string json = JsonUtility.ToJson(continueData);
        File.WriteAllText(continueDataFilePath, json);
    }

    // 加载 ContinueData
    public ContinueData LoadGame()
    {
        if (File.Exists(continueDataFilePath))
        {
            string json = File.ReadAllText(continueDataFilePath);
            return JsonUtility.FromJson<ContinueData>(json);
        }
        return null; // 如果文件不存在，返回 null
    }

    // 添加一个新的排名并保存
    public void AddRank(RankingListData newRank)
    {
        // 添加新的通关数据到排行榜
        ranksData.Add(newRank);
        // 按收集物品数量优先排序，如果数量相同，再按完成时间排序
        ranksData.Sort((a, b) =>
        {
            int collectionComparison = b.collectionAmount.CompareTo(a.collectionAmount); // 从大到小
            if (collectionComparison == 0)
            {
                return a.completeTime.CompareTo(b.completeTime); // 从小到大
            }
            return collectionComparison;
        });

        // 保存更新后的排行榜
        SaveRanking();
    }

    // 保存排行榜数据
    public void SaveRanking()
    {
        RankingListWrapper wrapper = new RankingListWrapper(ranksData);
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(rankingFilePath, json);
    }

    // 加载排行榜数据
    public List<RankingListData> LoadRanking()
    {
        if (File.Exists(rankingFilePath))
        {
            string json = File.ReadAllText(rankingFilePath);
            RankingListWrapper wrapper = JsonUtility.FromJson<RankingListWrapper>(json);
            return wrapper.rankingData;
        }
        return new List<RankingListData>(); // 如果文件不存在，返回空列表
    }
    public void ClearRankingData()
    {
        // 清空排行榜数据
        ranksData.Clear();

        // 保存清空后的排行榜数据到文件
        SaveRanking();

        // 如果需要，也可以选择删除排行榜文件
        if (File.Exists(rankingFilePath))
        {
            File.Delete(rankingFilePath);
        }
    }
    public void StartApplyingData()
    {
        // 启动协程
        StartCoroutine(ApplyDataWhenReady());
    }

    private IEnumerator ApplyDataWhenReady()
    {
        // 等待 SavePointManager, RewardManager, GameManager 实例全部不为空
        while (SavePointManager.Instance == null || RewardManager.Instance == null || GameManager.Instance == null)
        {
            yield return null;  // 等待下一帧继续检查
        }

        // 当所有实例都不为空时，执行应用数据的逻辑
        ApplyData();
    }

    private void ApplyData()
    {
        // 确保对象都已存在
        if (currentContinueData != null)
        {
            Debug.Log(currentContinueData.currentSavePos.GetVector3());
            SavePointManager.Instance.currentSavePoint = currentContinueData.currentSavePos.GetVector3();
            GameManager.Instance.completionTime = currentContinueData.completeTime;
            GameManager.Instance.deathCount = currentContinueData.deathCount;
            RewardManager.Instance.strawBerryAmount = currentContinueData.collectionAmount;

            // 可能包含一些初始化后的操作
            SavePointManager.Instance.OnDeath();
        }
        else
        {
            Debug.LogError("currentContinueData is null");
        }
    }

    // 删除保存的数据
    public void DeleteSaveData()
    {
        if (File.Exists(continueDataFilePath))
        {
            File.Delete(continueDataFilePath);
        }
        if (File.Exists(rankingFilePath))
        {
            File.Delete(rankingFilePath);
        }
    }
}


// 用于包装排行榜数据的辅助类
[System.Serializable]
public class RankingListWrapper
{
    public List<RankingListData> rankingData;

    public RankingListWrapper(List<RankingListData> rankingData)
    {
        this.rankingData = rankingData;
    }
}

// 包装 CollectionData 的类
[System.Serializable]
public class CollectionDataWrapper
{
    public List<CollectionData> collectionData;

    public CollectionDataWrapper(List<CollectionData> collectionData)
    {
        this.collectionData = collectionData;
    }
}
