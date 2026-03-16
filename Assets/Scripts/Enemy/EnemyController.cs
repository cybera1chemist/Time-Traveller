using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 1f;
    public float detectionRange = 30f;
    [Range(0f, 2f)] public float noiseStrength = 0.5f; // 移动噪音强度
    public float noiseFrequency = 2f; // 移动噪音频率
    
    [Header("攻击设置")]
    public float collideDamage = 10f;

    [Header("远程攻击设置")]
    public bool canRangedAttack = false;
    public float rangeAttackCooldown = 2f;
    public GameObject projectilePrefab;
    public ProjectileStats projectileStats;
    public GameObject firePoint;
    private float timer;
    private float firepointOriginalX;

    [Header("外形设置")]
    public bool differentSideSprites = false;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite leftSprite;  

    [Header("References")]
    [SerializeField] private GameObject expOrbPrefab;
    private EnemySpawner enemySpawner;
    
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Health health;
    private bool isPlayerDetected = false;
    private float noiseOffset;

    //敌人死亡时调用
    public System.Action<EnemyController> OnEnemyDeath;

    private SpriteRenderer sr;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.OnDeath += HandleDeath;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        enemySpawner.AddTotalEnemy();
        FindPlayer();
        noiseOffset = Random.Range(0f, 100f);
        if (canRangedAttack) firepointOriginalX = firePoint.transform.localPosition.x;
    }
    
    private void FixedUpdate()
    {
        if (playerTransform == null)  FindPlayer();

        if (playerTransform == null)  return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);      
        isPlayerDetected = distanceToPlayer <= detectionRange;
        
        if (isPlayerDetected)
        {
            MoveTowardsPlayer();
            if (canRangedAttack)
            {
                timer += Time.fixedDeltaTime;
                if (timer >= rangeAttackCooldown)
                {
                    Fire();
                    timer = 0f;
                }
            }
        }
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }
    
    private void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;
        
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // 添加移动噪音
        float noise = Mathf.PerlinNoise(Time.time * noiseFrequency, noiseOffset) * 2f - 1f;
        Vector2 perpendicular = new(-direction.y, direction.x);
        direction = (direction + noise * noiseStrength * perpendicular).normalized;

        rb.velocity = direction * moveSpeed;

        // 小怪sprite默认脸朝右
        if (direction.x < 0) // move left
        {
            if (!differentSideSprites)  sr.flipX = true;
            else  sr.sprite = leftSprite;
            if (canRangedAttack) firePoint.transform.localPosition = new Vector3(-firepointOriginalX, firePoint.transform.localPosition.y, firePoint.transform.localPosition.z);
        } else if (direction.x > 0) // move right
        {
            if (!differentSideSprites)  sr.flipX = false;
            else  sr.sprite = rightSprite;
            if (canRangedAttack) firePoint.transform.localPosition = new Vector3(firepointOriginalX, firePoint.transform.localPosition.y, firePoint.transform.localPosition.z);
        }
    }

    // 对玩家造成碰撞伤害
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<Health>(out var playerHealth))
            {
                playerHealth.TakeDamage(collideDamage);
            }
        }
    }
    
    private void Fire()
    {
        if (!canRangedAttack)
        {
            Debug.LogWarning("[EnemyController] This enemy can't perform ranged attacks!");
            return;
        }

        Debug.Log("[EnemyController] 敌人向玩家发射了一个子弹。");
        GameObject projectile = Instantiate(
            projectilePrefab, 
            firePoint.transform.position, 
            Quaternion.identity,
            transform);  // set enemy as parent for better organization
        if (projectile.TryGetComponent<Projectile>(out var proj))
        {
            proj.SetIsEnemyProjectile(true);
            proj.SetStats(projectileStats);
            proj.SetTarget(playerTransform);
        }


    }

    private void HandleDeath()
    {
        Instantiate(expOrbPrefab, transform.position, Quaternion.identity);
        GameStatsManager.Instance.AddKill();
        enemySpawner.RemoveTotalEnemy();
        
        Destroy(gameObject, 0.1f);
    }
}