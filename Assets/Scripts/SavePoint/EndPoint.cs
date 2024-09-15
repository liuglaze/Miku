using UnityEngine;
using UnityEngine.UI; // 需要引用UI命名空间
using System.Collections;

public class EndPoint : MonoBehaviour
{
    public Animator animator; // 终点动画的Animator
    public ScoreBoard scoreBoardUI; // 计分板UI的引用

    public bool hasTriggered = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true; // 防止重复触发
            // 播放通关动画
            //animator.SetTrigger("PlayEndAnimation");
            Debug.Log(1);
            EventManager.Instance.TriggerEvent("ReachSavePoint", ShowScoreBoard);
            ShowScoreBoard();
            DataManager.Instance.AddRank(new RankingListData(GameManager.Instance.GetDeathCount(),
                GameManager.Instance.GetCompletionTime(), GameManager.Instance.GetCollectedItems()));
            //EventManager.Instance.TriggerEvent("GameEnd");
        }
    }


    private void ShowScoreBoard()
    {
        // 显示计分板UI
        scoreBoardUI.gameObject.SetActive(true);

        // 获取数据并更新UI
        int collectedItems = GameManager.Instance.GetCollectedItems(); // 从GameManager获取已收集的道具数量
        int deathCount = GameManager.Instance.GetDeathCount(); // 从GameManager获取死亡次数
        float completionTime = GameManager.Instance.GetCompletionTime(); // 从GameManager获取通关时间
        scoreBoardUI.finishTime.text = completionTime.ToString();
        scoreBoardUI.deathCount.text = deathCount.ToString();
        scoreBoardUI.collectionAmount.text = collectedItems.ToString();
    }
}

