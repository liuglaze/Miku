using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomorin : MonoBehaviour
{
    public Rigidbody2D Miku;
    public float tomorinGravity = 1;

    public void Awake()
    {
        Miku = GameObject.Find("Miku").GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Miku.gravityScale += tomorinGravity;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Miku.gravityScale -= tomorinGravity;
    }
}
