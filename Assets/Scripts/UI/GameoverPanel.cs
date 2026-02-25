using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameoverPanel : MonoBehaviour
{
    [Header("Info")]
    public int levelID;
    public bool isWin;

    [Header("References")]
    public Image backgroundMask;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI surviveTimeText;
    public TextMeshProUGUI defeatedEnemiesText;
    public TextMeshProUGUI reachedLevelText;
    public GameObject uiWithinBattle;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        uiWithinBattle.SetActive(false);
        PauseController.Pause();

        // Set Background and Title
        Color c = new();
        if (isWin)
        {
            titleText.text = "跨时空行动：成功";
            c.r = 16f/255f;
            c.g = 101f/255f;
            c.b = 206f/255f;
            c.a = 100f/255f;
            backgroundMask.color = c;
        } else
        {
            titleText.text = "跨时空行动：失败";
            c.r = 83f/255f;
            c.b = 36f/255f;
            c.g = 0f/255f;
            c.a = 220f/255f;
            backgroundMask.color = c;
        }

        // Set texts
        levelNameText.text = $"关卡：{GameStatsManager.Instance.stateName}";
        surviveTimeText.text = $"存活时间：{FormatedTime(GameStatsManager.Instance.surviveTime)}";
        defeatedEnemiesText.text = $"击败敌人：{GameStatsManager.Instance.totalKills}个";
        reachedLevelText.text = $"最大等级：{GameStatsManager.Instance.reachedLevel}";
    }

    private string FormatedTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    public void LoadMainMenu()
    {
        PauseController.Resume();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/MainMenu/MainMenu");
    }
}
