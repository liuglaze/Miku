using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public bool isUsed=false;
    public Animator animator;
    public GameObject microPhone;

    private void Awake()
    {
        if(transform.childCount > 0)
            microPhone = transform.GetChild(0)?.gameObject;
    }

    private void Update()
    {
        if(microPhone)
        {
            microPhone.SetActive(isUsed);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isUsed)
        {
            //EventManager.Instance.TriggerEvent("ReachSavePoint");
            Debug.Log(SavePointManager.Instance.currentSavePoint);
            Debug.Log(transform.position);
            SavePointManager.Instance.currentSavePoint = transform.position;
            isUsed = true;
        }       
    }

}
