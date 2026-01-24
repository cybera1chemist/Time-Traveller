using UnityEngine;

// character stats are fixed, are initial attributes of a certain character;
// player stats are changable within battle
public struct PlayerStats
{
    public float maxHP;
    public float armor;
    public float moveSpeed;
    public float recovery;
    public float magnet;
    
    //攻击相关
    public float mightMultiplyer;
    public float speedMultiplyer;  // 弹道速度
    public float durationMultiplyer;
    public float cooldownMultiplyer;
    public float areaMultiplyer;  // 子弹大小 
    public float criticalChanceMultiplyer;
    public float criticalDamageMultiplyer;
}

[RequireComponent(typeof(Health))]
public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDB;
    [SerializeField] private PlayerStats playerStats;

    private int characterID;
    private CharacterStats characterStats;
     
    private void Start()
    {
        characterID = GlobalSettings.Instance.selectedCharacterID;
        characterStats = characterDB.GetCharacterStatsByID(characterID);
        InitializeStats();
    }

    private void InitializeStats()
    {
        playerStats.maxHP = characterStats.maxHP;
        playerStats.armor = characterStats.armor;
        playerStats.moveSpeed = characterStats.moveSpeed;
        playerStats.mightMultiplyer = characterStats.mightMultiplyer;
        playerStats.speedMultiplyer = characterStats.speedMultiplyer;
        playerStats.durationMultiplyer = characterStats.durationMultiplyer;
        playerStats.cooldownMultiplyer = characterStats.cooldownMultiplyer;
        playerStats.areaMultiplyer = characterStats.areaMultiplyer;
        playerStats.recovery = characterStats.recovery;
        playerStats.magnet = characterStats.magnet;
        playerStats.criticalChanceMultiplyer = characterStats.criticalChanceMultiplyer;
        playerStats.criticalDamageMultiplyer = characterStats.criticalDamageMultiplyer;

        Health health = GetComponent<Health>();
        health.SetMaxHealth(playerStats.maxHP);
    }

    #region 属性获取API
    public float GetMaxHealth() => playerStats.maxHP;
    public float GetArmor() => playerStats.armor;
    public float GetMoveSpeed() => playerStats.moveSpeed;
    public float GetMight() => playerStats.mightMultiplyer;
    public float GetSpeed() => playerStats.speedMultiplyer;
    public float GetDuration() => playerStats.durationMultiplyer;
    public float GetCooldown() => playerStats.cooldownMultiplyer;
    public float GetArea() => playerStats.areaMultiplyer;
    public float GetRecovery() => playerStats.recovery;
    public float GetMagnet() => playerStats.magnet;
    public float GetCriticalChance() => playerStats.criticalChanceMultiplyer;
    public float GetCriticalDamage() => playerStats.criticalDamageMultiplyer;
    #endregion

    #region 属性修改API
    public void AddMaxHealth(float amount)
    {
        playerStats.maxHP += amount;
        Health health = GetComponent<Health>();
        health.SetMaxHealth(playerStats.maxHP);
    }
    public void AddArmor(float amount) => playerStats.armor += amount;
    public void AddMoveSpeed(float amount) => playerStats.moveSpeed += amount;
    public void AddMight(float amount) => playerStats.mightMultiplyer += amount;
    public void AddSpeed(float amount) => playerStats.speedMultiplyer += amount;
    public void AddDuration(float amount) => playerStats.durationMultiplyer += amount;
    public void AddCooldown(float amount) => playerStats.cooldownMultiplyer += amount;
    public void AddArea(float amount) => playerStats.areaMultiplyer += amount;
    public void AddRecovery(float amount) => playerStats.recovery += amount;
    public void AddMagnet(float amount) => playerStats.magnet += amount;
    public void AddCriticalChance(float amount) => playerStats.criticalChanceMultiplyer += amount;
    public void AddCriticalDamage(float amount) => playerStats.criticalDamageMultiplyer += amount;
    #endregion
}
