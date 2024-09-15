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
    public SerializeVector3 currentSavePos;
    public int deathCount;
    public float completeTime;
    public int collectionAmount;
    public ContinueData(Vector3 currentSavePos,int deathCount,float completeTime,int collectionAmount)
    {
        this.currentSavePos = new SerializeVector3(currentSavePos);
        this.deathCount = deathCount;
        this.completeTime = completeTime;
        this.collectionAmount = collectionAmount;
    }
}
[Serializable]
public class RankingListData
{
    public int deathCount;
    public float completeTime;
    public int collectionAmount;

    public RankingListData(int deathCount, float completeTime, int collectionAmount)
    {
        this.deathCount = deathCount;
        this.completeTime = completeTime;
        this.collectionAmount = collectionAmount;
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
    public List<CollectionGuid> allCollections = new List<CollectionGuid>(); // ���ڴ洢����ע��� CollectionGuid
    private string continueDataFilePath;
    private string rankingFilePath;
    private string collectionFilePath;

    // �Զ���·�������� D:\unity\Miku\Assets\Data
    private string customDataDirectory = @"D:\unity\Miku\Assets\Data";

    override public void Awake()
    {
        base.Awake();
        // ȷ��Ŀ¼���ڣ�����������򴴽�
        if (!Directory.Exists(customDataDirectory))
        {
            Directory.CreateDirectory(customDataDirectory);
        }

        // ���� ContinueData �� Ranking��CollectionData ��·��
        continueDataFilePath = Path.Combine(customDataDirectory, "continueData.json");
        rankingFilePath = Path.Combine(customDataDirectory, "ranking.json");
        collectionFilePath = Path.Combine(customDataDirectory, "collection.json");
        currentContinueData = LoadGame();
        ranksData = LoadRanking();
        collectionData = LoadCollectionData();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        if (GameManager.Instance.loadData)
        {
            ApplyData();
        }
    }

    // ע�� CollectionGuid
    public void RegisterCollection(CollectionGuid collection)
    {
        if (!allCollections.Contains(collection))
        {
            allCollections.Add(collection);
        }
    }

    // �������� CollectionGuid ������
    public void SaveCollections()
    {
        collectionData.Clear();  // ���֮ǰ������

        // ��������ע��� CollectionGuid ����
        foreach (var collection in allCollections)
        {
            collectionData.Add(new CollectionData
            {
                guid = collection.GetGuid(),
                hasUsed = collection.hasCollect
            }) ;
        }

        // �� collectionData �б���Ϊ JSON �ļ�
        string json = JsonUtility.ToJson(new CollectionDataWrapper(collectionData));
        File.WriteAllText(collectionFilePath, json);
    }
    // ������� CollectionData
    public List<CollectionData> LoadCollectionData()
    {
        if (File.Exists(collectionFilePath))
        {
            string json = File.ReadAllText(collectionFilePath);
            CollectionDataWrapper wrapper = JsonUtility.FromJson<CollectionDataWrapper>(json);
            return wrapper.collectionData;
        }
        return new List<CollectionData>(); // ����ļ������ڣ����ؿ��б�
    }
    // ���� GUID ���� hasCollect ״̬
    public bool GetHasCollectedStatus(string guid)
    {
        // �����Ѽ��ص� collectionData �б�����ƥ��� GUID
        foreach (var data in collectionData)
        {
            if (data.guid == guid)
            {
                return data.hasUsed;
            }
        }
        return false; // Ĭ�Ϸ��� false����ʾû���ҵ����ռ�״̬
    }
    // ���� ContinueData
    public void SaveGame(ContinueData continueData)
    {
        string json = JsonUtility.ToJson(continueData);
        File.WriteAllText(continueDataFilePath, json);
    }

    // ���� ContinueData
    public ContinueData LoadGame()
    {
        if (File.Exists(continueDataFilePath))
        {
            string json = File.ReadAllText(continueDataFilePath);
            return JsonUtility.FromJson<ContinueData>(json);
        }
        return null; // ����ļ������ڣ����� null
    }

    // ���һ���µ�����������
    public void AddRank(RankingListData newRank)
    {
        // ����µ�ͨ�����ݵ����а�
        ranksData.Add(newRank);

        // ������º�����а�
        SaveRanking();
    }

    // �������а�����
    public void SaveRanking()
    {
        RankingListWrapper wrapper = new RankingListWrapper(ranksData);
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(rankingFilePath, json);
    }

    // �������а�����
    public List<RankingListData> LoadRanking()
    {
        if (File.Exists(rankingFilePath))
        {
            string json = File.ReadAllText(rankingFilePath);
            RankingListWrapper wrapper = JsonUtility.FromJson<RankingListWrapper>(json);
            return wrapper.rankingData;
        }
        return new List<RankingListData>(); // ����ļ������ڣ����ؿ��б�
    }

    public void ApplyData()
    {
        SavePointManager.Instance.currentSavePoint = currentContinueData.currentSavePos.GetVector3();
        GameManager.Instance.completionTime = currentContinueData.completeTime;
        GameManager.Instance.deathCount = currentContinueData.deathCount;
        RewardManager.Instance.strawBerryAmount = currentContinueData.collectionAmount;
        SavePointManager.Instance.OnDeath();
    }
    // ɾ�����������
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


// ���ڰ�װ���а����ݵĸ�����
[System.Serializable]
public class RankingListWrapper
{
    public List<RankingListData> rankingData;

    public RankingListWrapper(List<RankingListData> rankingData)
    {
        this.rankingData = rankingData;
    }
}

// ��װ CollectionData ����
[System.Serializable]
public class CollectionDataWrapper
{
    public List<CollectionData> collectionData;

    public CollectionDataWrapper(List<CollectionData> collectionData)
    {
        this.collectionData = collectionData;
    }
}
