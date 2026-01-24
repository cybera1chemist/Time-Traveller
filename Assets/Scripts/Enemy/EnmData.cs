using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int id;
    public string enemyName;
    public GameObject prefab;

    [Header("Stats")]
    public float maxHP = 10f;
    public float moveSpeed = 2f;
    public float conllideDamage = 10f;
    public float weaponDamage = 0f;
}
