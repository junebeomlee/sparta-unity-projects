#nullable enable
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler: MonoBehaviour
{
    public List<ItemSO> dropItemList = new();
    public int rewardGold;

    public (int, ItemSO?) GetReward()
    {
        if(dropItemList.Count <= 0) return (rewardGold, null);
        // 없을 경우 null 에러 발생
        return (rewardGold, dropItemList[Random.Range(0, dropItemList.Count)]);
    }
}