using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour
{
    [Header("language")]
    public GameObject languagePanel;
    public TextMeshProUGUI languagePanelTitle;

    [Header("Collection")]
    public GameObject collectionPanel;

    private void Start()
    {
        languagePanel.SetActive(false);
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/CharacterSelect/CharacterSelect");
    }

    public void OpenSettings()
    {
        languagePanel.SetActive(true);
    }

    public void OpenCollections()
    {
        // 需要解锁了角色海藻之后，才能解锁她的图鉴系统
        if (SaveManager.Instance.IsCharacterUnlocked(4)){
            collectionPanel.SetActive(true);
            Debug.Log("[MainMenu] 已打开图鉴终端。");
        }  else
        {
            AlertManager.Show("似乎还没有达成解锁图鉴系统的条件呢……");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("游戏已退出！");
    }

    #region Settting Language
    public void CloseLanguagePangel()
    {
        languagePanel.SetActive(false);
    }
    public void SelectChinese()
    {
        StartCoroutine(SetLanguage(0));
    }
    public void SelectEnglish()
    {
        StartCoroutine(SetLanguage(1));
    }
    public void SelectJapanese()
    {
        languagePanelTitle.text = "私は日本語がわかりませんでした！";
    }
    IEnumerator SetLanguage(int index)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale =
            LocalizationSettings.AvailableLocales.Locales[index];
    }
    
    #endregion
}
