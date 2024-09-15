using UnityEngine;
using UnityEngine.UI; // ��Ҫ����UI�����ռ�
using System.Collections;

public class EndPoint : MonoBehaviour
{
    public Animator animator; // �յ㶯����Animator
    public ScoreBoard scoreBoardUI; // �Ʒְ�UI������

    public bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true; // ��ֹ�ظ�����
            // ����ͨ�ض���
            //animator.SetTrigger("PlayEndAnimation");
            Debug.Log(1);
            EventManager.Instance.TriggerEvent("ReachSavePoint", ShowScoreBoard);
            ShowScoreBoard();
            //EventManager.Instance.TriggerEvent("GameEnd");
        }
    }


    private void ShowScoreBoard()
    {
        // ��ʾ�Ʒְ�UI
        scoreBoardUI.gameObject.SetActive(true);

        // ��ȡ���ݲ�����UI
        int collectedItems = GameManager.Instance.GetCollectedItems(); // ��GameManager��ȡ���ռ��ĵ�������
        int deathCount = GameManager.Instance.GetDeathCount(); // ��GameManager��ȡ��������
        float completionTime = GameManager.Instance.GetCompletionTime(); // ��GameManager��ȡͨ��ʱ��
        scoreBoardUI.finishTime.text = completionTime.ToString();
        scoreBoardUI.deathCount.text = deathCount.ToString();
        scoreBoardUI.collectionAmount.text = collectedItems.ToString();
    }
}

