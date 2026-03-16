/*
本脚本用于管理仅在 stage 3 中出现的特殊事件。
通用的关卡事件（如敌人生成）：
    前端：用StageConfig脚本化对象配置；
    后端：EnemySpawner.cs 进行处理。
*/

using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]

public class Stage3 : MonoBehaviour
{
    // References
    [SerializeField] private Dialog dialog;
    [SerializeField] private int startDialogID = 301; // 关卡3开场对话ID
    private EnemySpawner enemySpawner;

    [SerializeField] int heliosID = 8;
    private bool heliosSpawned = false;

    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        if (PlayerPrefs.GetInt($"Dialog_ID_{startDialogID}_Completed", 0).Equals(0)){
            // 关卡3开场对话没放过
            dialog.SetDialogID(startDialogID);
            dialog.hideAtStart = false;
        } else { // 放过了
            dialog.hideAtStart = true;
        }

    }

    private void Update()
    {
        // 第五分钟生成赫利俄斯
        if (!heliosSpawned && GameStatsManager.Instance.elapsedTime >= 300f)
        {
            SpawnHelios();
            heliosSpawned = true;
        }
    }

    private void SpawnHelios()
    {
        Debug.Log("[Stage3] 赫利俄斯已生成！");
        if (heliosSpawned) return;
        heliosSpawned = true;
        enemySpawner.SpawnSingleEnemy(heliosID);
    }

}
