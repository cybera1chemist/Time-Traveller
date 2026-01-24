using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class OwnedWeapon
{
    public int weaponID;
    public int currentLevel = 1;

    public OwnedWeapon(int id, int level)
    {
        weaponID = id;
        currentLevel = level;
    }
}

public class PlayerOwn : MonoBehaviour
{
    [Header("Owned Weapons")]
    public List<OwnedWeapon> ownedWeapons = new();

    [Header("References")]
    public GameObject basicWeaponPrefab;
    [SerializeField] private CharacterDatabase characterDB;

    private readonly List<GameObject> weaponInstances = new();

    [Header("Positioning")]
    public float radius = 0.5f;
    public float sectorAngle = 120f;
    public float offsetAngle = -60f;
    public float offsetY = 2.4f;

    [Header("UI")]
    [SerializeField] private List<GameObject> weaponImageHolders;

    const int MAX_WEAPON_CNT = 6;

    private void Start()
    {
        SetInitialWeapon();
        RefreshWeapons();
    }

    private void SetInitialWeapon()
    {
        if (ownedWeapons.Count != 0)
        {
            Debug.LogError("[PlayerOwn] 尝试在武器栏不为空时调用SetInitialWeapon！");
        }
        CharacterData characterData = characterDB.GetCharacterDataByID(GlobalSettings.Instance.selectedCharacterID);
        AddWeapon(characterData.initialWeaponID);
    }

    private void RefreshWeapons()
    {
        // 清空旧武器
        foreach (var obj in weaponInstances)
            Destroy(obj);
        weaponInstances.Clear();

        int count = ownedWeapons.Count;
        if (count == 0) return;

        float angleStep = sectorAngle / count;

        for (int i = 0; i < MAX_WEAPON_CNT; i++)
        {
            if (i >= count)
            {
                weaponImageHolders[i].GetComponent<Image>().color = Color.clear;
                continue;
            }

            // ===== 实例化武器 =====
            GameObject weaponObj = Instantiate(basicWeaponPrefab, transform.position, Quaternion.identity, transform);

            // ===== 根据 ScriptableObject 数据初始化 =====
            WeaponLoader wpLoader = weaponObj.GetComponent<WeaponLoader>();
            wpLoader.weaponID = ownedWeapons[i].weaponID;
            wpLoader.currentLevel = ownedWeapons[i].currentLevel;
            wpLoader.LoadWeapon();

            weaponInstances.Add(weaponObj);

            weaponImageHolders[i].GetComponent<Image>().color = Color.white;
            weaponImageHolders[i].GetComponent<Image>().sprite = wpLoader.weaponIcon;
        }
    }

    #region API for getting data
    public bool HasWeapon(int weaponID)
    {
        return ownedWeapons.Exists(w => w.weaponID == weaponID);
    }

    public List<int> OwnedWeaponIDs()
    {
        List<int> ids = new();
        foreach (var wp in ownedWeapons)
        {
            ids.Add(wp.weaponID);
        }
        return ids;
    }

    public bool IsWeaponMaxLevel(int weaponID, WeaponDatabase weaponDatabase)
    {
        var weapon = ownedWeapons.Find(w => w.weaponID == weaponID);
        if (weapon != null)
        {
            var wpData = weaponDatabase.GetWeaponData(weaponID);
            return weapon.currentLevel >= wpData.maxLevel;
        }
        else
        {
            Debug.LogWarning("[PlayerOwn] Check max level for a non-existing weapon: " + weaponID);
            return false;
        }
    }

    #endregion

    #region API for writing Data
    public void AddWeapon(int weaponID)
    {
        if (!ownedWeapons.Exists(w => w.weaponID == weaponID))
        {
            ownedWeapons.Add(new OwnedWeapon(weaponID, 1));
            RefreshWeapons();
        }
        else
        {
            Debug.LogWarning("[PlayerOwn] Try to add an existing weapon: " + weaponID);
        }
    }
    public void UpgradeWeapon(int weaponID)
    {
        var weapon = ownedWeapons.Find(w => w.weaponID == weaponID);
        if (weapon != null)
        {
            weapon.currentLevel++;
            RefreshWeapons();
        }
        else
        {
            Debug.LogWarning("[PlayerOwn] Try to upgrade a non-existing weapon: " + weaponID);
        }
    }
    #endregion

    
}
