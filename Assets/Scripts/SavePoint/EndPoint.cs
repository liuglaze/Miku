using UnityEngine;
using UnityEngine.UI; // ��Ҫ����UI�����ռ�
using System.Collections;

public class EndPoint : MonoBehaviour
{
    public Animator animator; // �յ㶯����Animator
    public GameObject scoreBoardUI; // �Ʒְ�UI������
    public Text collectedItemsText; // UI�ı���ʾ�ռ����ĵ�������
    public Text deathCountText; // UI�ı���ʾ��������
    public Text completionTimeText; // UI�ı���ʾͨ��ʱ��

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true; // ��ֹ�ظ�����
            // ����ͨ�ض���
            //animator.SetTrigger("PlayEndAnimation");
            ShowScoreBoard();
        }
    }


    private void ShowScoreBoard()
    {
        // ��ʾ�Ʒְ�UI
        scoreBoardUI.SetActive(true);

        // ��ȡ���ݲ�����UI
        int collectedItems = GameManager.Instance.GetCollectedItems(); // ��GameManager��ȡ���ռ��ĵ�������
        int deathCount = GameManager.Instance.GetDeathCount(); // ��GameManager��ȡ��������
        float completionTime = GameManager.Instance.GetCompletionTime(); // ��GameManager��ȡͨ��ʱ��

        // ����UI�ı�
        collectedItemsText.text = "Collected Items: " + collectedItems.ToString();
        deathCountText.text = "Death Count: " + deathCount.ToString();
        completionTimeText.text = "Completion Time: " + completionTime.ToString("F2") + "s";
    }
}

