using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    void Start()
    {
        if(GetComponent<Collider2D>() == null)
        {
            this.gameObject.AddComponent<BoxCollider2D>();
        }
    }


}
