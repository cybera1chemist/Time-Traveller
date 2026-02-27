/*
本脚本用于管理仅在 stage 1 中出现的特殊事件。
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

    // Zhuge
    [SerializeField] int zhugeEnemyId = 2;
    private bool zhugeSpawned = false;

    // Miao
    [SerializeField] int miaoEnemyId = 3;
    private bool miaoSpawned = false;

    // MrCrow
    [SerializeField] int crowEnemyId = 4;
    private bool crowSpawned = false;

    // Captain
    [SerializeField] int captainEnemyId = 5;
    private bool captainSpawned = false;

    public bool isKeyGet = false; // 是否已经获得了打开秘密房间的钥匙

    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        if (PlayerPrefs.GetInt("Dialog_ID_1_Completed", 0).Equals(0)){
            // 关卡1开场对话没放过
            dialog.SetDialogID(1);
            dialog.hideAtStart = false;
        } else { // 放过了
            dialog.hideAtStart = true;
        }

    }

    private void Update()
    {
        // 第两分半的时候，生成诸葛伪装成的怪物
        if (!zhugeSpawned && GameStatsManager.Instance.elapsedTime >= 150f) // 120 + 30 = 150
        {
            SpawnZhuge();
            zhugeSpawned = true;
        }

        // 第5分钟的时候，生成谜奥，但是测试的时候第一分钟的时候生成
        if (!miaoSpawned && GameStatsManager.Instance.elapsedTime >= 300f) // 5 * 60 = 300
        {
            SpawnMiao();
            miaoSpawned = true;
        }

        // 在第7.5分钟的时候生成第一个boss（小黑猫军师）
        if (!crowSpawned && GameStatsManager.Instance.elapsedTime >= 450f) // 7.5 * 60 = 450
        {
            SpawnCrow();
            crowSpawned = true;
        }

        // 在第10分钟的时候生成第二个boss（橘克船长）
        if (!captainSpawned && GameStatsManager.Instance.elapsedTime >= 599f) // 10 * 60 = 600
        {
            SpawnCaptain();
            captainSpawned = true;
        }
    }

    private void SpawnZhuge()
    {
        Debug.Log("[Stage1] 诸葛好帅已生成！");
        if (zhugeSpawned || SaveManager.Instance.IsCharacterUnlocked(2)) return;
        zhugeSpawned = true;
        enemySpawner.SpawnSingleEnemy(zhugeEnemyId);
    }

    private void SpawnMiao()
    {
        Debug.Log("[Stage1] 谜奥·秒描邈已生成！");
        if (miaoSpawned || SaveManager.Instance.IsCharacterUnlocked(3)) return;
        miaoSpawned = true;
        enemySpawner.SpawnSingleEnemy(miaoEnemyId);
    }

    private void SpawnCrow()
    {
        Debug.Log("[Stage1] 小黑猫军师（乌鸦先生）已生成！");
        if (crowSpawned) return;
        crowSpawned = true;
        enemySpawner.SpawnSingleEnemy(crowEnemyId);
    }

    private void SpawnCaptain()
    {
        Debug.Log("[Stage1] 橘克船长已生成！");
        if (captainSpawned) return;
        captainSpawned = true;
        enemySpawner.SpawnSingleEnemy(captainEnemyId);
    }
}
