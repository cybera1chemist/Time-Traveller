using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanelManager : MonoBehaviour
{
    [Header("Pages")]
    public GameObject homePage;
    public GameObject characterPage;
    public GameObject weaponPage;
    public GameObject enemyPage;

    private GameObject curPage;
    
    private void Awake()
    {
        gameObject.SetActive(false);
        curPage = homePage;
    }

    private void Start()
    {
        SwitchToHomePage();
        characterPage.SetActive(false);
        weaponPage.SetActive(false);
        enemyPage.SetActive(false);
    }

    #region "APIs for buttons"
    public void CloseCollectionPanel()
    {
        gameObject.SetActive(false);
    }

    public void SwitchToHomePage()
    {
        curPage.SetActive(false);
        homePage.SetActive(true);
        curPage = homePage;
    }

    public void SwitchToCharacterPage()
    {
        curPage.SetActive(false);
        characterPage.SetActive(true);
        curPage = characterPage;
    }

    public void SwitchToWeaponPage()
    {
        curPage.SetActive(false);
        weaponPage.SetActive(true);
        curPage = weaponPage;
    }

    public void SwitchToEnemyPage()
    {
        curPage.SetActive(false);
        enemyPage.SetActive(true);
        curPage = enemyPage;
    }
    #endregion
}
