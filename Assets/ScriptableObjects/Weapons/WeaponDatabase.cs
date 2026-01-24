using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Database/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    public List<WeaponData> allWeapons = new();

    // 运行时缓存
    private Dictionary<int, WeaponData> cache;

    public void Init()
    {
        cache = new Dictionary<int, WeaponData>();
        foreach (var w in allWeapons)
        {
            if (w == null) continue;
            if (cache.ContainsKey(w.weaponID))  continue;
            cache[w.weaponID] = w;
        }
    }

    public WeaponUpgrade GetWeaponUpgradeInfo(int weaponID, int level)
    {
        if (cache == null) Init();
        if (!cache.TryGetValue(weaponID, out var wd))
        {
            Debug.LogError($"[WeaponDatabase] weaponID {weaponID} not found");
            return null;
        }
        if (level <= 1)
        {
            Debug.LogError("[WeaponDatabase] Try to get upgrade info for level <= 1 !");
            return null;
        }
        return wd.GetUpgradeInfo(level);
    }

    public WeaponData GetWeaponData(int weaponID)
    {
        if (cache == null) Init();
        cache.TryGetValue(weaponID, out var wd);
        return wd;
    }

    public WeaponStats GetWeaponStatsAtLevel(int weaponID, int level)
    {
        if (cache == null) Init();
        if (!cache.TryGetValue(weaponID, out var wd))
        {
            Debug.LogError($"[WeaponDatabase] weaponID {weaponID} not found");
        }
        return wd.GetWpStatsAtLevel(level);
    }
}
