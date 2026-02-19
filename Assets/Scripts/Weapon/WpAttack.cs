using UnityEngine;

[RequireComponent(typeof(WeaponLoader))]

public class WpAttack : MonoBehaviour
{
    public GameObject projectilePrefab; 

    private WeaponLoader wp;
    private PlayerStatsManager playerStatsManager;
    private float timer = 1f;

    void Start()
    {
        wp = GetComponent<WeaponLoader>();

        // player is the parent of weapon
        playerStatsManager = transform.parent.GetComponent<PlayerStatsManager>();

        // initialize timer randomly
        timer = Random.Range(0f, wp.cooldown);
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;

        Health target = FindClosestEnemy();
        if (target == null) return;  // no enemy in range, don't attack

        timer = wp.cooldown * playerStatsManager.GetCooldown();
        AttackOnce(target); 
    }

    void AttackOnce(Health target)
    {
        float finalMight = wp.might * playerStatsManager.GetMight();
        float finalCriticalChance = wp.criticalChance + playerStatsManager.GetCriticalChance();
        float finalCriticalDamage = wp.criticalDamage + playerStatsManager.GetCriticalDamage();

        bool isCritical = Random.value < finalCriticalChance;
        float damage = isCritical ? finalMight * finalCriticalDamage : finalMight;

        Fire(target, damage);

    }

    private Health FindClosestEnemy()
    {
        Vector3 playerPosition = transform.parent.position;
        playerPosition.y += 0.24f;  // The center of player
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerPosition, wp.range, LayerMask.GetMask("Enemy"));

        Health closest = null;
        float minDist = float.MaxValue;

        foreach (var h in hits)
        {
            float d = Vector2.Distance(transform.position, h.transform.position);
            if (d < minDist)
            {
                minDist = d;
                closest = h.GetComponent<Health>();
            }
        }
        return closest;
    }

    private void Fire(Health target, float damage)
    {
        GameObject projObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile proj = projObj.GetComponent<Projectile>();
        proj.SetDamage(damage);
        proj.SetSpeed(wp.speed * playerStatsManager.GetSpeed());
        proj.SetDuration(wp.duration * playerStatsManager.GetDuration());
        proj.SetRange(wp.range * playerStatsManager.GetArea());
        proj.SetSprite(wp.projectileSprite);
        proj.SetTarget(target.transform);
    }

}
