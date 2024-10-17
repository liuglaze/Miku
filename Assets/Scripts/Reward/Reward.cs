using System.Collections;
using Unity.VisualScripting;
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
    private Coroutine coroutine;
    private Vector3 velocity = Vector3.zero;
    private void Awake()
    {
        collectionGuid = GetComponent<CollectionGuid>();
        if(collectionGuid.hasCollect)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
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
        if (collectionGuid.hasCollect)
        {
            gameObject.SetActive(false);
        }
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
        if (collision.CompareTag("Player")&&(coroutine == null))
        {
            // 设定跟随目标
            Debug.Log("enter");
            followTarget = collision.transform;
            isFollowing = true;
            totalRewards = RewardManager.Instance.GetTotalRewards(); // 更新总数
            // 启动跟随协程
        }
    }
    private void FixedUpdate()
    {
        if (isFollowing && followTarget != null)
        {
            FollowTarget();
        }
    }
    public void ReachSavePoint()
    {
        if(isFollowing)
        {
            OnCollect();
        }
    }
    private void FollowTarget()
    {
        float angleStep = 360f / totalRewards;
        float angle = rewardIndex * angleStep;
        float radian = angle * Mathf.Deg2Rad;

        Vector3 targetPosition = followTarget.position + new Vector3(
            Mathf.Cos(radian) * followDistance,
            Mathf.Sin(radian) * followDistance,
            0);

        // 使用 SmoothDamp 代替 Lerp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.3f); // 0.3f 是平滑时间
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
        coroutine = null;
    }

    private void OnPlayerDied()
    {
        isFollowing = false;
        StopAllCoroutines(); // 确保协程停止
        coroutine = StartCoroutine(ReturnToOriginalPosition());
    }
}




