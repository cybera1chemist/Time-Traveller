using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class Boss : MonoBehaviour
{
    public int bossID;
    [Header("Dialog")]
    [SerializeField] private bool playDialogAtStart;
    [SerializeField] private int startDialogID;
    [SerializeField] private bool playDialogOnTriggerEnter;
    [SerializeField] private int triggerDialogID;
    [SerializeField] private bool playDialogOnDeath;
    [SerializeField] private int deathDialogID;

    [Header("Alert")]
    [SerializeField] private bool playAlertOnDeath;
    [SerializeField] private string alertMessage;

    private Health health;
    private Dialog dialog;
    private GameStatsManager manager;

    private bool hasPlayedTriggerDialog = false;

    private void Start()
    {
        manager = FindObjectOfType<GameStatsManager>();
        health = GetComponent<Health>();

        if (playDialogAtStart)
        {
            PlayStartDialog();
        }

        // 死亡事件在End函数中。要么先放对话，要么不放对话直接End，反正End函数是必定触发的。
        if (playDialogOnDeath) {
            health.OnDeath += PlayEndDialog;
        } else
        {
            health.OnDeath += End;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playDialogOnTriggerEnter && collision.CompareTag("Player") && !hasPlayedTriggerDialog)
        {
            PlayTriggerDialog();
            hasPlayedTriggerDialog = true;
        }
    }

    private void PlayStartDialog()
    {
        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        if (dialog == null)  Debug.LogWarning("[Boss] Can't find dialog using Find object of Type!");

        dialog.PlayDialogID(startDialogID);
    }

    private void PlayTriggerDialog()
    {
        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        if (dialog == null)  Debug.LogWarning("[Boss] Can't find dialog using Find object of Type!");

        dialog.PlayDialogID(triggerDialogID);
    }

    private void PlayEndDialog()
    {
        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        dialog.OnDialogCompleted += End;

        if (dialog == null)  Debug.LogWarning("[Boss] Can't find dialog using Find object of Type!");

        dialog.PlayDialogID(deathDialogID);
    }

    private void End()
    {
        SaveManager.Instance.SetBossDefeated(bossID);
        SaveManager.Instance.Save();

        if (playAlertOnDeath) AlertManager.Show(alertMessage);
    }

    private void OnDestroy()
    {
        if (health != null)  health.OnDeath -= PlayEndDialog;
        if (dialog != null) dialog.OnDialogCompleted -= End;
    }
}
