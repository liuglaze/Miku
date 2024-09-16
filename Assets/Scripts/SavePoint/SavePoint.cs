using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public bool isUsed=false;
    public Animator animator;
    public GameObject microPhone;
    public CollectionGuid collectionGuid;
    private void Awake()
    {
        microPhone = transform.GetChild(0).gameObject;
        collectionGuid=GetComponent<CollectionGuid>();
    }
    private void OnEnable()
    {
        isUsed = collectionGuid.hasCollect;
    }
    private void Start()
    {
        microPhone.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isUsed)
        {
            microPhone.SetActive(true);
            EventManager.Instance.TriggerEvent("ReachSavePoint");
            SavePointManager.Instance.currentSavePoint = transform.position;
            isUsed = true;
            collectionGuid.hasCollect = true;
        }       
    }

}
