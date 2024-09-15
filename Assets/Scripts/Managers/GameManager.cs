using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int deathCount = 0; // �����������
    public float startTime; // ��Ϸ��ʼ��ʱ��
    public float completionTime; // ͨ��ʱ��

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
    }

    private void Start()
    {
        // ��¼��Ϸ��ʼʱ��
        startTime = Time.time;
    }



    // ��Ϸ����ʱ���ã���¼ͨ��ʱ��
    public void CompleteLevel()
    {
        completionTime = Time.time - startTime;
    }

    // ��ȡ���ռ����ߵ�����
    public int GetCollectedItems()
    {
        Debug.Log(RewardManager.Instance.strawBerryAmount);
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

