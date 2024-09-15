using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : Singleton<SavePointManager>
{
    public Transform currentSavePoint;
    public Transform player;

    private void Start()
    {
        currentSavePoint = transform;
    }

    private void OnEnable()
    {
        EventManager.Instance.AddEvent("Death", OnDeath);
    }

    public void OnDeath()
    {
        player.transform.position = currentSavePoint.position;
        player.GetComponent<HatsuneController>().canMove = true;
    }
}
