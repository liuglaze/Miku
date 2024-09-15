using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public bool isUsed=false;
    public Animator animator;
    public Reward reward;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isUsed)
        {
            //播放动画
            if(reward!=null&&reward.isFollowing)
            {
                Destroy(reward.gameObject);
            }
            SavePointManager.Instance.currentSavePoint = transform;
        }       
    }

}
