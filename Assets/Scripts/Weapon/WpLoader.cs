// Weapon's attributes are stored here
using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    [Header("Identity")]
    public int weaponID;
    public int currentLevel = 1;
    
    [Header("Basic Information")]
    public ProjectileType projectileType;
    public int maxLevel;

    [Header("Attack Attributes")]
    public float might;
    public float speed;
    public float duration;
    public float area;
    public float cooldown;
    public int amount;
    public float criticalChance;
    public float criticalDamage;
    public float range;

    [Header("Visuals")]
    public Sprite weaponIcon;
    public Sprite projectileSprite;

    [Header("References")]
    public WeaponDatabase database;  // 从 Inspector 拖进来

    // private WpVisual wpVisual;

    void Start()
    {
        // LoadWeapon();
    }

    public void LoadWeapon()
    {
        // Load data from database and check
        if (database == null)
        {
            Debug.LogError("WeaponLoader: 数据库没有拖进来！");
            return;
        }
        var weaponData = database.GetWeaponData(weaponID);
        if (weaponData == null)
        {
            Debug.LogError($"WeaponLoader: 找不到武器ID {weaponID} 的数据！");
            return;
        }
        var wpStats = database.GetWeaponStatsAtLevel(weaponID, currentLevel);

        projectileType = weaponData.projectileType;
        maxLevel = weaponData.maxLevel;

        might = wpStats.might;
        speed = wpStats.speed;
        duration = wpStats.duration;
        area = wpStats.area;
        cooldown = wpStats.cooldown;
        amount = wpStats.amount;
        criticalChance = wpStats.criticalChance;
        criticalDamage = wpStats.criticalDamage;
        range = wpStats.range;

        weaponIcon = weaponData.weaponIcon;
        projectileSprite = weaponData.projectileSprite;
    }

    public void LevelUp()
    {
        currentLevel++;
        LoadWeapon();
    }
}
