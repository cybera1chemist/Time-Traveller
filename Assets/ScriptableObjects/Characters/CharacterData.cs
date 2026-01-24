using UnityEngine;

[System.Serializable]
public struct CharacterStats
{
    public int CharacterID;
    public float maxHP;
    public float armor;
    public float moveSpeed;
    public float recovery;
    public float magnet;

    // Attributes that affect atack
    public float mightMultiplyer;
    public float speedMultiplyer;
    public float durationMultiplyer;
    public float cooldownMultiplyer;
    public float areaMultiplyer;
    
    public float criticalChanceMultiplyer;
    public float criticalDamageMultiplyer;
}

[CreateAssetMenu(fileName="CharacterSO", menuName="SO/CharacterSO")]
public class CharacterData : ScriptableObject
{
    [Header("Identity")]
    public int ID;
    public string characterName;

    [Header("Visual")]
    public Sprite idleSprite;
    public RuntimeAnimatorController animatorController;

    [Header("战斗相关")]
    public CharacterStats stats = new();
    public int initialWeaponID;
}
