using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class Boss : MonoBehaviour
{
    [Header("Dialog")]
    [SerializeField] private bool playDialogAtStart;
    [SerializeField] private int startDialogID;
    [SerializeField] private bool playDialogOnDeath;
    [SerializeField] private int deathDialogID;

    [Header("Alert")]
    [SerializeField] private bool playAlertOnDeath;
    [SerializeField] private string alertMessage;

    private Health health;
    private Dialog dialog;
    private GameStatsManager manager;
    public EnemyData enemyData;

    void Start()
    {
        manager = FindObjectOfType<GameStatsManager>();
        health = GetComponent<Health>();

        // 死亡事件
        if (playDialogOnDeath) {
            health.OnDeath += PlayDialog;
        } else
        {
            health.OnDeath += End;
        }
        
    }

    private void PlayDialog()
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
        SaveManager.Instance.UnlockCharacter(2);
        SaveManager.Instance.Save();

        if (playAlertOnDeath) AlertManager.Show(alertMessage);
    }

    private void OnDestroy()
    {
        if (health != null)  health.OnDeath -= PlayDialog;
        if (dialog != null) dialog.OnDialogCompleted -= End;
    }
}
