using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public bool isUsed=false;
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isUsed)
        {
            //播放动画
            SavePointManager.Instance.currentSavePoint = transform;

        }       
    }

}
