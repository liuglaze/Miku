using UnityEngine;
using UnityEngine.UI; // 需要引用UI命名空间
using System.Collections;

public class EndPoint : MonoBehaviour
{
    public Animator animator; // 终点动画的Animator
    public GameObject scoreBoardUI; // 计分板UI的引用
    public Text collectedItemsText; // UI文本显示收集到的道具数量
    public Text deathCountText; // UI文本显示死亡次数
    public Text completionTimeText; // UI文本显示通关时间

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true; // 防止重复触发
            // 播放通关动画
            //animator.SetTrigger("PlayEndAnimation");
            ShowScoreBoard();
        }
    }


    private void ShowScoreBoard()
    {
        // 显示计分板UI
        scoreBoardUI.SetActive(true);

        // 获取数据并更新UI
        int collectedItems = GameManager.Instance.GetCollectedItems(); // 从GameManager获取已收集的道具数量
        int deathCount = GameManager.Instance.GetDeathCount(); // 从GameManager获取死亡次数
        float completionTime = GameManager.Instance.GetCompletionTime(); // 从GameManager获取通关时间

        // 更新UI文本
        collectedItemsText.text = "Collected Items: " + collectedItems.ToString();
        deathCountText.text = "Death Count: " + deathCount.ToString();
        completionTimeText.text = "Completion Time: " + completionTime.ToString("F2") + "s";
    }
}

