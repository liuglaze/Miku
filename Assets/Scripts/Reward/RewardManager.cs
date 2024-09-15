using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager>
{
    public int strawBerryAmount = 0;
    private List<Reward> rewards = new List<Reward>(); // ¸úËæµÄ²ÝÝ®ÁÐ±í
    public int GetRewardIndex(Reward reward)
    {
        if (!rewards.Contains(reward))
        {
            rewards.Add(reward);
        }
        return rewards.IndexOf(reward);
    }

    public int GetTotalRewards()
    {
        return rewards.Count;
    }

}
