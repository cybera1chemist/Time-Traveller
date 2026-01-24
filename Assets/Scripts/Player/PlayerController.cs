using System;
using UnityEngine;
 
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStatsManager))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDB;

    private int selectedCharacterID;

    // Components
    private Animator animator;
    private Health health;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerStatsManager playerStatsManager;
    private Vector2 movement;

    private static readonly int is_right = Animator.StringToHash("is_right");

    public Action<PlayerController> onPlayerDeath;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        animator = GetComponent<Animator>();

        health.OnDeath += Health_OnDeath;
    }

    private void Start()
    {
        selectedCharacterID = GlobalSettings.Instance.selectedCharacterID;
        CharacterData characterData = characterDB.GetCharacterDataByID(selectedCharacterID);
        sr.sprite = characterData.idleSprite;
        animator.runtimeAnimatorController = characterData.animatorController;
    }

    private void Health_OnDeath()
    {
        Debug.Log("Player Death");
        onPlayerDeath?.Invoke(this);
    }

    private void Update()
    {
        // 获取输入
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if( Mathf.Approximately(rb.velocity.magnitude, 0f) ) { 
            animator.SetBool("is_walking", false);
        }
        else
        {
            animator.SetBool("is_walking", true);
        }
        if (rb.velocity.x > 0)  animator.SetBool(is_right, true);
        if (rb.velocity.x < 0)  animator.SetBool(is_right, false);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // 从PlayerStats获取实际移动速度
        float actualMoveSpeed = playerStatsManager.GetMoveSpeed();       
        rb.velocity = movement.normalized * actualMoveSpeed;
    }

}