using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    private void Awake()
    {
        // 确保物体上有 Collider2D 组件
        Collider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        // 设置 Collider 为 Trigger
        collider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventManager.Instance.TriggerEvent("Death");
    }
}
