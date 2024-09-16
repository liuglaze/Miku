using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public Transform followTarget; // ����Ŀ�꣨������ϵ�Transform��
    public float followDistance = 2.0f; // ��ݮ�����Ŀ��֮�����С����
    private Vector3 originalPosition; // ��ݮ��ԭʼλ��
    public bool isFollowing = false; // �Ƿ����ڸ���
    private int rewardIndex; // ÿ����ݮ������
    private int totalRewards; // ��ǰ����Ĳ�ݮ����
    public CollectionGuid collectionGuid;
    private void Awake()
    {
        collectionGuid = GetComponent<CollectionGuid>();
    }
    private void OnEnable()
    {
        EventManager.Instance.AddEvent("ReachSavePoint", ReachSavePoint);
        // ������������¼�
        EventManager.Instance.AddEvent("Death", OnPlayerDied);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent("ReachSavePoint", ReachSavePoint);
        // ȡ��������������¼�
        EventManager.Instance.RemoveEvent("Death", OnPlayerDied);
    }

    private void Start()
    {
        if (collectionGuid.hasCollect)
        {
            gameObject.SetActive(false);
        }
        // �����ݮ��ԭʼλ��
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
            OnCollect();
        }
    }
    private IEnumerator FollowTarget()
    {
        Vector3 velocity = Vector3.zero; // ���� SmoothDamp ���ٶȲ���
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

                // ʹ�� SmoothDamp ƽ���ƶ���Ŀ��λ�ã�ȷ�����ᡰ˲�ơ�
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.1f); // 0.2f ��ƽ��ʱ�䣬��ֵԽС����Խ��

                // ���Ե���ƽ��ʱ�䣨�� 0.2f�������ݸ����ٶ����������
            }

            yield return null; // ÿ֡����
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




