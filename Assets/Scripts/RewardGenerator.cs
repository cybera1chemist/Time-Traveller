using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RewardGenerator
{
    // TODO: 现在只抽取武器，所以只需要int id就可以定位到武器，
    // 未来若增加抽取道具，则需要更改逻辑

    // TODO: 若player的某种武器已满级，则不出现在奖励池中
    public static List<int> Generate(int numberOfRewards, int maxID, List<int> excludedIDs = null)
    {
        List<int> rewards = new();

        List<int> pool = new(maxID);
        for (int id=1; id<=maxID; id++)
        {
            pool.Add(id);
        }
        
        for (int i = 0; i < numberOfRewards; i++)
        {
            if (pool.Count == 0){
                rewards.Add(-1);
                continue;
            }
            int idx = Random.Range(0, pool.Count);
            rewards.Add(pool[idx]);
            pool.RemoveAt(idx);
        }

        return rewards;
    }
}
