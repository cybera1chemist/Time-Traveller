using System;
using System.Collections.Generic;
using UnityEngine;

#region Helper Data Structure to Handle Upgrade
public enum WeaponStat
{
    Might,
    Speed,
    Duration,
    Area,
    Cooldown,
    Amount,
    CriticalChance,
    CriticalDamage,
    Range
}

public enum UpgradeOp
{
    ADD,
    MULTIPLY
}

[Serializable]
public class WeaponUpgrade
{
    public WeaponStat stat;
    public UpgradeOp op;
    public float value;
}
#endregion

[CreateAssetMenu(fileName="WeaponSO", menuName="SO/WeaponSO")]
public class WeaponData : ScriptableObject
{
    [Header("Identity")]
    public int weaponID;
    public string weaponName;

    [Header("Information")]
    public string weaponDescription;
    public int maxLevel = 5;

    [Header("Visuals")]
    public Sprite weaponIcon;
    public Sprite projectileSprite;

    [Header("Level 1 Base Stats")]
    public float might;
    public float speed;
    public float duration;
    public float area;
    public float cooldown;
    public int amount;
    public float criticalChance;
    public float criticalDamage;
    public float range;

    [Header("Level Upgrade Data")]
    public List<WeaponUpgrade> upgrades = new();

    private static readonly Dictionary<WeaponStat, StatAccessor> statMap =  new()
    {
        { WeaponStat.Might,          new StatAccessor(s => s.might,          (s,v)=> s.might = v) },
        { WeaponStat.Speed,          new StatAccessor(s => s.speed,          (s,v)=> s.speed = v) },
        { WeaponStat.Duration,       new StatAccessor(s => s.duration,       (s,v)=> s.duration = v) },
        { WeaponStat.Area,           new StatAccessor(s => s.area,           (s,v)=> s.area = v) },
        { WeaponStat.Cooldown,       new StatAccessor(s => s.cooldown,       (s,v)=> s.cooldown = v) },
        { WeaponStat.Amount,         new StatAccessor(s => s.amount,         (s,v)=> s.amount = Mathf.RoundToInt(v)) },
        { WeaponStat.CriticalChance, new StatAccessor(s => s.criticalChance, (s,v)=> s.criticalChance = v) },
        { WeaponStat.CriticalDamage, new StatAccessor(s => s.criticalDamage, (s,v)=> s.criticalDamage = v) },
        { WeaponStat.Range,          new StatAccessor(s => s.range,          (s,v)=> s.range = v) }
    };

    private void OnValidate()
    {
        if (upgrades.Count != maxLevel-1)
            Debug.LogWarning($"[WeaponData] {weaponName} levels count {upgrades.Count} does not match maxLevel {maxLevel}");
    }

    private void ApplyUpgrade(WeaponStats s, WeaponUpgrade u)
    {
        var acc = statMap[u.stat];

        float current = acc.getter(s);
        float next = (u.op == UpgradeOp.ADD) ? current + u.value : current * u.value;

        acc.setter(s, next);
    }

    #region API for others
    public WeaponUpgrade GetUpgradeInfo(int level)
    {
        if (level < 1 || level > maxLevel)
        {
            Debug.LogError($"Requested level {level} is out of bounds for weapon {weaponName} with max level {maxLevel}.");
            return null;
        }
        if (level == 1)
        {
            Debug.LogWarning($"Level 1 weapon {weaponName} has no upgrade info.");
            return null;
        }
        return upgrades[level-1];
    }

    public WeaponStats GetWpStatsAtLevel(int level)
    {
        if (level < 1 || level > maxLevel)
        {
            Debug.LogError($"Requested level {level} is out of bounds for weapon {weaponName} with max level {maxLevel}.");
            return default;
        }

        WeaponStats s = new()
        {
            might = might,
            speed = speed,
            duration = duration,
            area = area,
            cooldown = cooldown,
            amount = amount,
            criticalChance = criticalChance,
            criticalDamage = criticalDamage,
            range = range
        };

        for (int i = 1; i < level; i++)
        {
            ApplyUpgrade(s, upgrades[i-1]);  
        }

        return s;
    }
    #endregion
}

public struct WeaponStats
{
    public float might;
    public float speed;
    public float duration;
    public float area;
    public float cooldown;
    public int amount;
    public float criticalChance;
    public float criticalDamage;
    public float range;
}

public struct StatAccessor
{
    public Func<WeaponStats, float> getter;
    public Action<WeaponStats, float> setter;

    public StatAccessor(Func<WeaponStats, float> g, Action<WeaponStats, float> s)
    {
        getter = g;
        setter = s;
    }
}
