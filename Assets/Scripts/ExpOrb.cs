using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public int expValue = 1;      
    public float moveSpeed = 3f;    

    private Transform target;       // 玩家位置
    private bool isFollowing = false;

    void FixedUpdate()
    {
        if (isFollowing && target != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void StartFollowing(Transform player)
    {
        target = player;
        isFollowing = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // When collected, grant experience to the player
            ExperienceSystem expSystem = collision.GetComponent<ExperienceSystem>();
            expSystem.GainExp(expValue);

            Destroy(gameObject);
        }
    }
}
