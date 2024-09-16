using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankList : MonoBehaviour
{
    public GameObject RankingItemPrefab;
    public Transform RankingContent; // ScrollView 的 Content 节点
    private void OnEnable()
    {
        UpdateRankingUI();
    }
    public void UpdateRankingUI()
    {
        // 清空之前的排名项
        foreach (Transform child in RankingContent)
        {
            Destroy(child.gameObject);
        }

        // 为每一个排行榜数据生成一个新的 UI 项
        foreach (var rank in DataManager.Instance.ranksData)
        {
            GameObject newItem = Instantiate(RankingItemPrefab, RankingContent);

            // 假设 RankingItemPrefab 有三个 Text 组件用于显示数据
            var texts = newItem.GetComponentsInChildren<Text>();
            texts[3].text =  rank.completeTime.ToString("F2") + "s";
            texts[2].text = "收集物品数量:" + rank.collectionAmount.ToString();
            texts[1].text =  "死亡次数:" + rank.deathCount.ToString();
            texts[0].text = "玩家:" + rank.playerID;
        }
    }
}