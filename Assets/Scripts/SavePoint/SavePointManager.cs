using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : Singleton<SavePointManager>
{
    public Vector3 currentSavePoint;
    public Transform player;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Miku").GetComponent<Transform>();
    }

    private void Start()
    {
        //currentSavePoint = player.transform.position;
    }

    private void OnEnable()
    {
        //EventManager.Instance.AddEvent("Death", OnDeath);
    }

    public void OnDeath()
    {
        player.transform.position = currentSavePoint;
        HatsuneController hatsune =player.GetComponent<HatsuneController>();
        hatsune.canMove = true;
        hatsune.rb.gravityScale = 2f;
    }
}
