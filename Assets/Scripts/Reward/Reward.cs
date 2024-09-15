using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour
{
    public Transform followTarget; // ����Ŀ�꣨������ϵ�Transform��
    public float followDistance = 2.0f; // ��ݮ�����Ŀ��֮�����С����
    private Vector3 originalPosition; // ��ݮ��ԭʼλ��
    public bool isFollowing = false; // �Ƿ����ڸ���
    private void Start()
    {
        // �����ݮ��ԭʼλ��
        originalPosition = transform.position;
        // ������������¼�
        EventManager.Instance.AddEvent("Death", OnPlayerDied);
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
            followTarget = collision.transform.Find("FollowTarget");
            isFollowing = true;
            // ��������Э��
            StartCoroutine(FollowTarget());
        }
    }

    private IEnumerator FollowTarget()
    {
        while (isFollowing)
        {
            if (followTarget != null)
            {
                // �����ݮ�����Ŀ��֮��ľ���
                float distance = Vector3.Distance(transform.position, followTarget.position);

                if (distance != followDistance)
                {
                    // ����Ŀ��λ�ã�����ָ���ĸ������
                    Vector3 direction = (transform.position - followTarget.position).normalized;
                    Vector3 targetPosition = followTarget.position + direction * followDistance;
                    // ����ݮƽ���ƶ���Ŀ��λ��
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f); // 5f��ƽ�����ӣ����Ե���
                }
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
