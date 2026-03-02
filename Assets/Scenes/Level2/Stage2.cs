/*
本脚本用于管理仅在 stage 2 中出现的特殊事件。
通用的关卡事件（如敌人生成）：
    前端：用StageConfig脚本化对象配置；
    后端：EnemySpawner.cs 进行处理。
*/

using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]

public class Stage2 : MonoBehaviour
{
    // References
    [SerializeField] private Dialog dialog;
    private EnemySpawner enemySpawner;

    // Boss 1
    [SerializeField] int boss1ID = 8;
    private bool boss1Spawned = false;

    // Boss 2
    [SerializeField] int boss2ID = 7;
    private bool boss2Spawned = false;

    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        if (PlayerPrefs.GetInt("Dialog_ID_201_Completed", 0).Equals(0)){
            // 关卡2开场对话没放过
            dialog.SetDialogID(201);
            dialog.hideAtStart = false;
        } else { // 放过了
            dialog.hideAtStart = true;
        }

    }

    private void Update()
    {
        // 第五分钟生成 Boss1
        if (!boss1Spawned && GameStatsManager.Instance.elapsedTime >= 300f)
        {
            SpawnBoss1();
            boss1Spawned = true;
        }

        // 第10分钟：生成 Boss2
        if (!boss2Spawned && GameStatsManager.Instance.elapsedTime >= 600f)
        {
            SpawnBoss2();
            boss2Spawned = true;
        }
    }

    private void SpawnBoss1()
    {
        Debug.Log("Boss 1 已生成！");
        if (boss1Spawned) return;
        boss1Spawned = true;
        enemySpawner.SpawnSingleEnemy(boss1ID);
    }

    private void SpawnBoss2()
    {
        Debug.Log("Boss 2 已生成！");
        if (boss2Spawned) return;
        boss2Spawned = true;
        enemySpawner.SpawnSingleEnemy(boss2ID);
    }

}
