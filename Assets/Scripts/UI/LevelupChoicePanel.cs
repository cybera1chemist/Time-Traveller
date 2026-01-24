using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelupChoicePanel : MonoBehaviour
{
    [Header("References to UI")]
    public Image icon;
    public TMP_Text wpName;
    public TMP_Text wpLevel;
    public TMP_Text wpDescription;
    public Button selectButton;
    public AudioSource sfxPlayer;

    [Header("References to Information")]
    public WeaponDatabase weaponDatabase;

    private LevelUpUIManager manager;
    private PlayerOwn player_own;

    private int weaponID;
    private int curLevel;  // if curLevel = -1, it is a new weapon
    private bool isInvalid = false;

    public void Setup(int id, LevelUpUIManager uiManager)
    {
        manager = uiManager;
        if (id==-1) {
            isInvalid = true;
            icon.sprite = null;
            wpName.text = "No Available Weapon";
            wpLevel.text = "";
            wpDescription.text = "You have already maxed out all weapons!";
            selectButton.interactable = false;
            return;
        }

        // Find what player already owns
        player_own = FindObjectOfType<PlayerOwn>();
        weaponID = id;
        if (!player_own.HasWeapon(id)) curLevel = -1;
        else
        {
            var owned_wp = player_own.ownedWeapons.Find(w => w.weaponID == id);
            curLevel = owned_wp.currentLevel;
        }

        // Get weapon data
        var wpData = weaponDatabase.GetWeaponData(id);
        if (curLevel + 1 >= wpData.maxLevel)
        {
            Debug.LogWarning("[LevelupChoicePanel] Weapon " + id + " is already at max level!");
            return;
        }
        

        // Set up UI
        icon.sprite = wpData.weaponIcon;
        wpName.text = wpData.weaponName;
        if (curLevel < 1)
        {
            wpLevel.text = "New!";
            wpDescription.text = wpData.weaponDescription;
        }
        else
        {
            var upgradeInfo = weaponDatabase.GetWeaponUpgradeInfo(id, curLevel+1);
            wpLevel.text = "Level " + (curLevel + 1).ToString();
            wpDescription.text = ParseWeaponUpgrade(upgradeInfo);
        }
        
    }

    public void OnSelect()
    {
        if (isInvalid) return;
        sfxPlayer.Play();
        // Add or upgrade weapon
        if (curLevel < 1)
        {
            player_own.AddWeapon(weaponID);
        }
        else
        {
            player_own.UpgradeWeapon(weaponID);
        }
        manager.Close();
    }

    private string ParseWeaponUpgrade(WeaponUpgrade u)
    {
        string result = "";
        switch (u.stat)
        {
            case WeaponStat.Might:
                result += "伤害 ";
                break;
            case WeaponStat.Speed:
                result += "弹道速度 ";
                break;
            case WeaponStat.Duration:
                result += "子弹持续时间 ";
                break;
            case WeaponStat.Area:
                result += "子弹大小 ";
                break;
            case WeaponStat.Cooldown:
                result += "冷却 ";
                break;
            case WeaponStat.Amount:
                result += "子弹数量 ";
                break;
            case WeaponStat.CriticalChance:
                result += "暴击率 ";
                break;
            case WeaponStat.CriticalDamage:
                result += "暴击伤害 ";
                break;
            case WeaponStat.Range:
                result += "攻击范围 ";
                break;
        }
        float v = u.value;
        switch(u.op)
        {
            case UpgradeOp.ADD:
                if (v > 0) result += "+ ";
                else result += "- ";
                result += Mathf.Abs(v).ToString("F1");
                break;
            case UpgradeOp.MULTIPLY:
                if (v > 1) result += "+ ";
                else result += "- ";
                result += (Mathf.Abs(v - 1) * 100).ToString("F0") + "%";
                break;
        }
        return result;
    }
}
