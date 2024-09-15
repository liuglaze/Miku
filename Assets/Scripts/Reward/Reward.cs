using System.Collections;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public Transform followTarget; // 跟随目标（玩家身上的Transform）
    public float followDistance = 2.0f; // 草莓与跟随目标之间的最小距离
    private Vector3 originalPosition; // 草莓的原始位置
    public bool isFollowing = false; // 是否正在跟随
    private int rewardIndex; // 每个草莓的索引
    private int totalRewards; // 当前跟随的草莓总数
    public CollectionGuid collectionGuid;
    private void OnEnable()
    {
        collectionGuid = GetComponent<CollectionGuid>();
        EventManager.Instance.AddEvent("ReachSavePoint", ReachSavePoint);
        // 订阅玩家死亡事件
        EventManager.Instance.AddEvent("Death", OnPlayerDied);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent("ReachSavePoint", ReachSavePoint);
        // 取消订阅玩家死亡事件
        EventManager.Instance.RemoveEvent("Death", OnPlayerDied);
    }
    private void Start()
    {
        // 保存草莓的原始位置
        originalPosition = transform.position;
        rewardIndex = RewardManager.Instance.GetRewardIndex(this);
        totalRewards = RewardManager.Instance.GetTotalRewards();
    }

    private void OnCollect()
    {
        collectionGuid.hasCollect = true;
        RewardManager.Instance.strawBerryAmount++;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 设定跟随目标
            followTarget = collision.transform;
            isFollowing = true;
            totalRewards = RewardManager.Instance.GetTotalRewards(); // 更新总数
            // 启动跟随协程
            StartCoroutine(FollowTarget());
        }
    }
    public void ReachSavePoint()
    {
        if(isFollowing)
        {
            OnCollect();
        }
    }
    private IEnumerator FollowTarget()
    {
        while (isFollowing)
        {
            if (followTarget != null)
            {
                // 计算草莓与跟随目标的圆周排列位置
                float angleStep = 360f / totalRewards; // 每个草莓的角度间隔
                float angle = rewardIndex * angleStep; // 当前草莓的角度
                // 将角度转换为弧度
                float radian = angle * Mathf.Deg2Rad;

                // 计算目标位置，基于角度的极坐标转换为笛卡尔坐标
                Vector3 targetPosition = followTarget.position + new Vector3(
                    Mathf.Cos(radian) * followDistance,
                    Mathf.Sin(radian) * followDistance,
                    0);

                // 将草莓平滑移动到目标位置
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f); // 5f是平滑因子，可以调整
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




