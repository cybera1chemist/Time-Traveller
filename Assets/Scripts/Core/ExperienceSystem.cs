using UnityEngine;
using System;

[RequireComponent(typeof(PlayerStatsManager))]
public class ExperienceSystem : MonoBehaviour
{
    [Header("Reference")]
    public LevelUpUIManager levelupUI;
    public AudioSource sfxPlayer;

    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float currentExp = 0;

    private PlayerStatsManager playerStatsManager;
    private ExpBar expBar;

    private void Awake()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
        if (playerStatsManager == null)
        {
            Debug.LogError("PlayerStats component not found on the same GameObject!");
        }
        expBar = FindObjectOfType<ExpBar>();
    }

    public void Start()
    {
        expBar.UpdateExpBar(GetRatio(), currentLevel);
    }

    public void GainExp(int expAmount)
    {
        currentExp += expAmount;
        expBar.UpdateExpBar(GetRatio(), currentLevel);

        if ( currentExp >= GetCurrentLevelRequirement())
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExp -= GetCurrentLevelRequirement();
        currentLevel++;
        GameStatsManager.Instance.SetLevel(currentLevel);
        
        levelupUI.Show();
        
        expBar.UpdateExpBar(GetRatio(), currentLevel);
        if (sfxPlayer != null)
        {
            sfxPlayer.Play();
        }
    }

    public float GetCurrentLevelRequirement()
    {
        // 一些示例：
        // 1级：3；2级：3.75；3级：4.6875；4级：5.859375
        // 5级：7.32421875；6级：9.1552734375
        float level1Exp = 4f;
        float growth = 1.3f;
        return level1Exp * Mathf.Pow(growth, currentLevel - 1);
    }

    private float GetRatio()
    {
        return currentExp / GetCurrentLevelRequirement();
    }

}

