using UnityEngine;

[System.Serializable]
public struct ProjectileStats
{
    public float duration;
    public float speed;
    public float damage;
    public float range;
}

[RequireComponent(typeof(SpriteRenderer))]
public class Projectile : MonoBehaviour
{
    public ProjectileStats projStats;
    public bool isEnemyProjectile = false;  // 标记是否为敌人发射的子弹，决定碰撞逻辑

    private Vector3 direction;
    private bool hasHit = false;
    
    private void Start()
    {
        // 设置子弹生命周期
        Destroy(gameObject, projStats.duration);
    }

    private void FixedUpdate()
    {
        transform.position += projStats.speed * Time.deltaTime * direction;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 防止多次触发
        if (hasHit) return;
        
        if (collision.CompareTag("Enemy") && !isEnemyProjectile)
        {
            if (collision.TryGetComponent<Health>(out var health))
            {
                hasHit = true;
                health.TakeDamage(projStats.damage);
                
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("Player") && isEnemyProjectile)
        {
            if (collision.TryGetComponent<Health>(out var health))
            {
                Debug.Log("[Projectile] 玩家被敌人子弹击中，造成了 " + projStats.damage + " 点伤害。");
                hasHit = true;
                health.TakeDamage(projStats.damage);
                
                Destroy(gameObject);
            }
        }
    }

    #region 属性修改API
    public void SetIsEnemyProjectile(bool isEnemy) => isEnemyProjectile = isEnemy;
    public void SetDuration(float duration) => projStats.duration = duration;
    public void SetDamage(float newDamage) => projStats.damage = newDamage;
    public void SetSpeed(float newSpeed) => projStats.speed = newSpeed;
    public void SetRange(float newRange) => projStats.range = newRange;
    public void SetSprite(Sprite newSprite)
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = newSprite;
    }
    
    public void SetStats(ProjectileStats newStats) => projStats = newStats;

    public void SetTarget(Transform target)
    {
        direction = (target.position - transform.position).normalized;
        transform.right = direction;
    }
    #endregion
}