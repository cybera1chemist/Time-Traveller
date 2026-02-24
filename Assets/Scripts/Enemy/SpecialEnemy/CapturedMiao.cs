using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class CapturedMiao : MonoBehaviour
{
    private Health health;
    private Dialog dialog;
    private GameStatsManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameStatsManager>();
        health = GetComponent<Health>();

        health.OnDeath += PlayDialog;
    }
    private void PlayDialog()
    {
        Debug.Log("[CapturedMiao] 绑架谜奥·秒描邈的海盗已被击败。");

        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        dialog.OnDialogCompleted += End;

        if (dialog == null)
        {
            Debug.LogWarning("Can't find dialog using Find object of Type!");
        }

        dialog.PlayDialogID(3);
    }

    private void End()
    {
        SaveManager.Instance.UnlockCharacter(3);
        SaveManager.Instance.Save();
        dialog.OnDialogCompleted -= End;

        AlertManager.Show("已解锁新角色谜奥·秒描邈！\n在新的一局游戏中，可在角色选择界面查看。");
    }
}
