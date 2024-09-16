using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankList : MonoBehaviour
{
    public GameObject RankingItemPrefab;
    public Transform RankingContent; // ScrollView �� Content �ڵ�
    public Button backBtn;

    private void OnEnable()
    {
        UpdateRankingUI();
    }
    public void UpdateRankingUI()
    {
        // ���֮ǰ��������
        foreach (Transform child in RankingContent)
        {
            Destroy(child.gameObject);
        }

        // Ϊÿһ�����а���������һ���µ� UI ��
        foreach (var rank in DataManager.Instance.ranksData)
        {
            GameObject newItem = Instantiate(RankingItemPrefab, RankingContent);

            // ���� RankingItemPrefab ������ Text ���������ʾ����
            var texts = newItem.GetComponentsInChildren<Text>();
            texts[3].text =  rank.completeTime.ToString("F2") + "s";
            texts[2].text = "�ռ���Ʒ����:" + rank.collectionAmount.ToString();
            texts[1].text =  "��������:" + rank.deathCount.ToString();
            texts[0].text = "���:" + rank.playerID;
        }
    }
}