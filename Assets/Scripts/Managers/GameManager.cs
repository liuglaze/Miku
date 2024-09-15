using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int deathCount = 0; // �����������
    public float completionTime; // ͨ��ʱ��
    public bool loadData=false;
    private void Awake()
    {
        // ȷ��ֻ��һ��GameManagerʵ�����ڣ�����ģʽ��
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        completionTime += Time.deltaTime;
    }

    // ��ȡ���ռ����ߵ�����
    public int GetCollectedItems()
    {
        return RewardManager.Instance.strawBerryAmount;
    }

    // ��ȡ�����������
    public int GetDeathCount()
    {
        return deathCount;
    }

    // ��ȡͨ��ʱ��
    public float GetCompletionTime()
    {
        return completionTime;
    }
}

