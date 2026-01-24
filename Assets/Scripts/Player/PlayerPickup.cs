using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PickupArea : MonoBehaviour
{
    [SerializeField] float initial_radius = 1f;
    private PlayerStatsManager playerStats;
    private CircleCollider2D pickupArea;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStatsManager>();
        pickupArea = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        // TODO: 以后可改成只有当player的magnet属性变化时才更新，
        // 以免一直update浪费性能
        pickupArea.radius = playerStats.GetMagnet() * initial_radius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ExperienceOrb>(out var orb))
        {
            orb.StartFollowing(playerStats.transform);
        }
    }
}
