using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicAppearanceObstacle : StaticObstacle
{
    public float appearDuration = 2f;  // 物体出现的持续时间
    public float disappearDuration = 3f;  // 物体消失的持续时间

    private Renderer objectRenderer;   // 用于控制物体的可见性
    public Collider2D coll;

    private void Start()
    {
        // 获取物体的 Renderer 组件
        objectRenderer = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();

        // 启动协程，控制物体的周期性出现和消失
        StartCoroutine(AppearAndDisappearRoutine());
    }

    private IEnumerator AppearAndDisappearRoutine()
    {
        while (true)
        {
            // 让物体出现
            objectRenderer.enabled = true;  // 启用 Renderer，使物体可见
            coll.enabled = true;
            yield return new WaitForSeconds(appearDuration);  // 等待出现的时间

            // 让物体消失
            objectRenderer.enabled = false;  // 禁用 Renderer，使物体不可见
            coll.enabled = false;
            yield return new WaitForSeconds(disappearDuration);  // 等待消失的时间
        }
    }
}
