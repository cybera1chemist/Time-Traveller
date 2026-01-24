using UnityEngine;

public enum ProjectileType {LINEAR, ORBITAL, HOMING}

[SerializeField]
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
    public ProjectileType motionType;

    private Vector3 direction;
    private bool hasHit = false;
    
    private void Start()
    {
        // 设置子弹生命周期
        Destroy(gameObject, projStats.duration);
    }

    private void FixedUpdate()
    {
        if (motionType == ProjectileType.LINEAR) transform.position += projStats.speed * Time.deltaTime * direction;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 防止多次触发
        if (hasHit) return;
        
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(projStats.damage);
                hasHit = true;

                Destroy(gameObject);
            }
        }
    }

    #region 属性修改API
    public void SetDuration(float duration) => projStats.duration = duration;
    public void SetDamage(float newDamage) => projStats.damage = newDamage;
    public void SetSpeed(float newSpeed) => projStats.speed = newSpeed;
    public void SetRange(float newRange) => projStats.range = newRange;
    public void SetSprite(Sprite newSprite)
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = newSprite;
    }
    public void SetTarget(Transform target)
    {
        direction = (target.position - transform.position).normalized;
        transform.right = direction;
    }
    #endregion
}