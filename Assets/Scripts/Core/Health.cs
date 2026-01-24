using UnityEngine;
using System;

/// 通用生命值组件 - 玩家和敌人共用
public class Health : MonoBehaviour
{
    [Header("生命值设置")]
    public float maxHealth = 100f;
    public float currentHealth;

    public GameoverPanel gameoverPanel;
    
    public event Action<float, float> OnHealthChanged; // 当前血量, 最大血量
    public event Action OnDeath; // 死亡事件
    
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    public bool IsDead { get; private set; }
    public float HealthPercentage => currentHealth / maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        // 初始血量通知
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    /// 受到伤害 - 统一伤害接口
    public void TakeDamage(float amount)
    {
        if (IsDead) return;
        
        currentHealth = Mathf.Max(0f, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
        if (currentHealth <= 0f)  Die();
    }
    
    /// 治疗 - 统一治疗接口
    public void Heal(float amount)
    {
        if (IsDead) return;
        
        float oldHealth = currentHealth;
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
    }
    
    public void Die()
    {
        IsDead = true;
        currentHealth = 0f;
        OnDeath?.Invoke();
        
        // 如果是玩家死亡，就结束游戏
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("[Health] 玩家死了！");
            GameStatsManager.Instance.StopCounting();
            gameoverPanel.isWin = false;
            gameoverPanel.Show();
        }
    }
    
    public void Revive(float healthPercent = 1f)
    {
        if (!IsDead) return;
        
        currentHealth = Mathf.Max(1f, maxHealth * healthPercent);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    public void FullRestore()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    #region API
    public void SetMaxHealth(float hp)
    {
        maxHealth = hp;
        currentHealth = maxHealth;
    }
    #endregion
}
