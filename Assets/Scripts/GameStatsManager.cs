using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatsManager : MonoBehaviour
{
    public static GameStatsManager Instance { get; private set; }
    public EdgeCollider2D EdgeColl { get; private set; }
    public float leftBorder { get; private set; }
    public float rightBorder { get; private set; }
    public float topBorder { get; private set; }
    public float bottomBorder { get; private set; }

    [Header("Info")]
    public int levelID = 1;

    [Header("Settings")]
    public int maxMinite = 10;

    [Header("Game Stats")]
    public string stateName;
    public float surviveTime;
    public float elapsedTime;
    public int totalKills;
    public int reachedLevel;

    [Header("References")]
    public TextMeshProUGUI timeText;
    public GameoverPanel gameoverPanel;
    public Dialog dialogPanel;

    private bool isRunning = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ResetAll();
        isRunning = true;
        
        GameObject mapBorders = GameObject.Find("MapBorders");

        Transform leftBorderTransform = null;
        Transform rightBorderTransform = null;
        Transform topBorderTransform = null;
        Transform bottomBorderTransform = null;

        foreach (Transform child in mapBorders.transform) {
            if (child.name == "LeftBorder") {
                leftBorderTransform = child;
            } else if (child.name == "RightBorder") {
                rightBorderTransform = child;
            }else if (child.name == "TopBorder") {
                topBorderTransform = child;
            } else if (child.name == "BottomBorder") {
                bottomBorderTransform = child;
            }
        }

        EdgeCollider2D leftCollider = leftBorderTransform.GetComponent<EdgeCollider2D>();
        EdgeCollider2D rightCollider = rightBorderTransform.GetComponent<EdgeCollider2D>();
        EdgeCollider2D topCollider = topBorderTransform.GetComponent<EdgeCollider2D>();
        EdgeCollider2D bottomCollider = bottomBorderTransform.GetComponent<EdgeCollider2D>();

        leftBorder = leftCollider.bounds.max.x;
        rightBorder = rightCollider.bounds.min.x;
        topBorder = topCollider.bounds.min.y;
        bottomBorder = bottomCollider.bounds.max.y;

    }

    private void Update()
    {
        if (isRunning)  {
            elapsedTime += Time.deltaTime;
            surviveTime = elapsedTime;
            timeText.text = $"{FormatedTime(surviveTime)}";   
        }

        if (surviveTime > maxMinite * 60f)
        {
            // 检查场上是否还有敌人
            Boss boss = FindObjectOfType<Boss>();
            if (boss == null)
            {
                Win();
            }
        }
    }

    private void Win()
    {
        isRunning = false;
        PauseController.Pause();
        Debug.Log("[GameStatsManager] 达到存活时间上限，游戏胜利！");

        gameoverPanel.levelID = levelID;
        gameoverPanel.isWin = true;
        gameoverPanel.Show();

        switch (levelID)
        {
            case 1:
                if (SaveManager.Instance.IsCharacterUnlocked(4))
                {
                    PlayerPrefs.SetInt("Stage2_Unlocked", 1);
                    AlertManager.Show("已解锁下一段时空！\n可在新一局的关卡选择界面查看。");
                } else
                {
                    AlertManager.Show("这个时空似乎还有值得探索的地方……");
                }
                break;
            // case 2:
            //     PlayerPrefs.SetInt("Stage2_Unlocked", 1);
            //     break;
            // case 3:
            //     PlayerPrefs.SetInt("Stage3_Unlocked", 1);
            //     break;
        }
    }

    private string FormatedTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    #region API
    public void StartCounting() => isRunning = true;
    public void StopCounting() => isRunning = false;
    public void AddKill() => totalKills++;
    public void SetLevel(int l) => reachedLevel = l;

    public void ResetAll()
    {
        surviveTime = 0f;
        elapsedTime = 0f;
        totalKills = 0;
        reachedLevel = 0;
        isRunning = false;
    }
    public void ActivateDialogPanel()
    {
        dialogPanel.gameObject.SetActive(true);
    }
    #endregion
    
}
