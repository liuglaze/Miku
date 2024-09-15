using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour
{
    public Transform followTarget; // 跟随目标（玩家身上的Transform）
    public float followDistance = 2.0f; // 草莓与跟随目标之间的最小距离
    private Vector3 originalPosition; // 草莓的原始位置
    public bool isFollowing = false; // 是否正在跟随
    private void Start()
    {
        // 保存草莓的原始位置
        originalPosition = transform.position;
        // 订阅玩家死亡事件
        EventManager.Instance.AddEvent("Death", OnPlayerDied);
    }

    private void OnDestroy()
    {
        // 取消订阅玩家死亡事件
        EventManager.Instance.RemoveEvent("Death", OnPlayerDied);
        RewardManager.Instance.strawBerryAmount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 设定跟随目标
            followTarget = collision.transform.Find("FollowTarget");
            isFollowing = true;
            // 启动跟随协程
            StartCoroutine(FollowTarget());
        }
    }

    private IEnumerator FollowTarget()
    {
        while (isFollowing)
        {
            if (followTarget != null)
            {
                // 计算草莓与跟随目标之间的距离
                float distance = Vector3.Distance(transform.position, followTarget.position);

                if (distance != followDistance)
                {
                    // 计算目标位置，保持指定的跟随距离
                    Vector3 direction = (transform.position - followTarget.position).normalized;
                    Vector3 targetPosition = followTarget.position + direction * followDistance;
                    // 将草莓平滑移动到目标位置
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f); // 5f是平滑因子，可以调整
                }
            }

            yield return null;
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            // 平滑返回到原始位置
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * 5f); // 5f是平滑因子，可以调整
            yield return null;
        }
        // 确保最终位置精确
        transform.position = originalPosition;
    }

    private void OnPlayerDied()
    {
        isFollowing = false;
        StartCoroutine(ReturnToOriginalPosition());
    }
}
