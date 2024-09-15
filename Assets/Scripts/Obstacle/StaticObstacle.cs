using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    private void Awake()
    {
        // ȷ���������� Collider2D ���
        Collider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        // ���� Collider Ϊ Trigger
        collider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventManager.Instance.TriggerEvent("Death");
    }
}
