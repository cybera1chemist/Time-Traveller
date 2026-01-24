using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class Zhuge : MonoBehaviour
{
    public Sprite pirateSprite;
    public Sprite zhugeSprite;  

    private Health health;
    private SpriteRenderer sr;
    private Dialog dialog;
    private GameStatsManager manager;
    public EnemyData enemyData;

    void Start()
    {
        manager = FindObjectOfType<GameStatsManager>();
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();

        // 死亡事件
        health.OnDeath += PlayDialog;
        
    }

    private void PlayDialog()
    {
        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        dialog.OnDialogCompleted += End;

        if (dialog == null)  Debug.LogWarning("Zhuge can't find dialog using Find object of Type!");

        sr.sprite = zhugeSprite;

        dialog.PlayDialogID(2);
    }

    private void End()
    {
        sr.sprite = pirateSprite;
        SaveManager.Instance.UnlockCharacter(2);
        SaveManager.Instance.Save();

        AlertManager.Show("已解锁新角色诸葛好帅！\n在新的一局游戏中，可在角色选择界面查看。");
    }

    private void OnDestroy()
    {
        if (health != null)  health.OnDeath -= PlayDialog;
        if (dialog != null) dialog.OnDialogCompleted -= End;
    }
}
