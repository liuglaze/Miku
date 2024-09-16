using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicAppearanceObstacle : StaticObstacle
{
    public float appearDuration = 2f;  // ������ֵĳ���ʱ��
    public float disappearDuration = 3f;  // ������ʧ�ĳ���ʱ��

    private Renderer objectRenderer;   // ���ڿ�������Ŀɼ���
    public Collider2D coll;

    private void Start()
    {
        // ��ȡ����� Renderer ���
        objectRenderer = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();

        // ����Э�̣���������������Գ��ֺ���ʧ
        StartCoroutine(AppearAndDisappearRoutine());
    }

    private IEnumerator AppearAndDisappearRoutine()
    {
        while (true)
        {
            // ���������
            objectRenderer.enabled = true;  // ���� Renderer��ʹ����ɼ�
            coll.enabled = true;
            yield return new WaitForSeconds(appearDuration);  // �ȴ����ֵ�ʱ��

            // ��������ʧ
            objectRenderer.enabled = false;  // ���� Renderer��ʹ���岻�ɼ�
            coll.enabled = false;
            yield return new WaitForSeconds(disappearDuration);  // �ȴ���ʧ��ʱ��
        }
    }
}
