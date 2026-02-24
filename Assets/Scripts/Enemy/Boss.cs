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

    void Start()
    {
        manager = FindObjectOfType<GameStatsManager>();
        health = GetComponent<Health>();

        if (playDialogAtStart)
        {
            PlayStartDialog();
        }

        // 死亡事件
        if (playDialogOnDeath) {
            health.OnDeath += PlayEndDialog;
        } else
        {
            health.OnDeath += End;
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
        SaveManager.Instance.Save();

        if (playAlertOnDeath) AlertManager.Show(alertMessage);
    }

    private void OnDestroy()
    {
        if (health != null)  health.OnDeath -= PlayEndDialog;
        if (dialog != null) dialog.OnDialogCompleted -= End;
    }
}
