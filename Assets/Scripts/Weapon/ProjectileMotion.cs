using UnityEngine;

[RequireComponent(typeof(WeaponLoader))]
[RequireComponent(typeof(SpriteRenderer))]

public class ProjectileMotion : MonoBehaviour
{
    private SpriteRenderer sr;
    private WeaponLoader wp;
    private Vector3 originalPosition;

    private float damage;
    private float speed;
    private Health target;
    private bool canDealDamage = false;

    private void Start()
    {
        wp = GetComponent<WeaponLoader>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = wp.weaponIcon;
        originalPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            canDealDamage = false;
        }
        if (canDealDamage)
        {
            // Dash to the enemy
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.transform.position,
                speed * Time.deltaTime
            );
        }
        if (!canDealDamage)
            transform.localPosition = originalPosition;
    }

    public void SetSprite(Sprite newSprite) {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = newSprite;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canDealDamage && col.CompareTag("Enemy"))
        {
            if (col.TryGetComponent<Health>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
                canDealDamage = false;
            }
            else
            {
                canDealDamage = false;
            }
        }
    }

}
