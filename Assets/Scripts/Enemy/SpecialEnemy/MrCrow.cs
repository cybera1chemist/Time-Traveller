using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class MrCrow : MonoBehaviour
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
        Debug.Log("[MrCrow] 海盗军师【乌鸦先生】已被击败。");

        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        dialog.OnDialogCompleted += End;

        if (dialog == null)
        {
            Debug.LogWarning("Can't find dialog using Find object of Type!");
        }

        dialog.PlayDialogID(4);
    }

    private void End()
    {
        Stage1 stage1 = FindObjectOfType<Stage1>();
        if (stage1 == null)       {
            Debug.LogWarning("MrCrow can't find Stage1 using Find object of Type!");
            return;
        }
        stage1.isKeyGet = true;
        
        AlertManager.Show("已获得道具：打开秘密房间的钥匙！");
        SaveManager.Instance.Save();
    }
}
