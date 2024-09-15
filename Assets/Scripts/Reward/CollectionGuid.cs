using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGuid : MonoBehaviour
{
    public string guid;
    public bool hasCollect;
    private void Awake()
    {
        DataManager.Instance.RegisterCollection(this);
        if(GameManager.Instance.loadData)
        {
            hasCollect = DataManager.Instance.GetHasCollectedStatus(guid);
        }
    }
    private void OnValidate()
    {
        GenerateGuid();
    }
    public string GetGuid()
    {
        return guid;
    }
    public void GenerateGuid()
    {
        if (string.IsNullOrEmpty(guid))
        {
            guid = Guid.NewGuid().ToString();
        }
    }
}
