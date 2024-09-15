using System.Collections;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public Transform followTarget; // ����Ŀ�꣨������ϵ�Transform��
    public float followDistance = 2.0f; // ��ݮ�����Ŀ��֮�����С����
    private Vector3 originalPosition; // ��ݮ��ԭʼλ��
    public bool isFollowing = false; // �Ƿ����ڸ���
    private int rewardIndex; // ÿ����ݮ������
    private int totalRewards; // ��ǰ����Ĳ�ݮ����

    private void OnEnable()
    {
        EventManager.Instance.AddEvent("ReachSavePoint", ReachSavePoint);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent("ReachSavePoint", ReachSavePoint);
    }
    private void Start()
    {
        // �����ݮ��ԭʼλ��
        originalPosition = transform.position;
        // ������������¼�
        EventManager.Instance.AddEvent("Death", OnPlayerDied);
        rewardIndex = RewardManager.Instance.GetRewardIndex(this);
        totalRewards = RewardManager.Instance.GetTotalRewards();
    }

    private void OnDestroy()
    {
        // ȡ��������������¼�
        EventManager.Instance.RemoveEvent("Death", OnPlayerDied);
        RewardManager.Instance.strawBerryAmount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �趨����Ŀ��
            followTarget = collision.transform;
            isFollowing = true;
            totalRewards = RewardManager.Instance.GetTotalRewards(); // ��������
            // ��������Э��
            StartCoroutine(FollowTarget());
        }
    }
    public void ReachSavePoint()
    {
        if(isFollowing)
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator FollowTarget()
    {
        while (isFollowing)
        {
            if (followTarget != null)
            {
                // �����ݮ�����Ŀ���Բ������λ��
                float angleStep = 360f / totalRewards; // ÿ����ݮ�ĽǶȼ��
                float angle = rewardIndex * angleStep; // ��ǰ��ݮ�ĽǶ�
                // ���Ƕ�ת��Ϊ����
                float radian = angle * Mathf.Deg2Rad;

                // ����Ŀ��λ�ã����ڽǶȵļ�����ת��Ϊ�ѿ�������
                Vector3 targetPosition = followTarget.position + new Vector3(
                    Mathf.Cos(radian) * followDistance,
                    Mathf.Sin(radian) * followDistance,
                    0);

                // ����ݮƽ���ƶ���Ŀ��λ��
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f); // 5f��ƽ�����ӣ����Ե���
            }

            yield return null;
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            // ƽ�����ص�ԭʼλ��
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * 5f); // 5f��ƽ�����ӣ����Ե���
            yield return null;
        }
        // ȷ������λ�þ�ȷ
        transform.position = originalPosition;
    }

    private void OnPlayerDied()
    {
        isFollowing = false;
        StartCoroutine(ReturnToOriginalPosition());
    }
}




