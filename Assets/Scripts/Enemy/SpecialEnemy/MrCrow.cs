/*
因为乌鸦先生击败后会掉落门禁卡，所以归类在special enemy里面，
而不适用通用的Boss.cs。
*/

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

        PlayFirstDialog();

        health.OnDeath += PlayDialog;
    }

    private void PlayFirstDialog()
    {
        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();

        if (dialog == null)
        {
            Debug.LogWarning("Can't find dialog using Find object of Type!");
        }

        dialog.PlayDialogID(4);
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

        dialog.PlayDialogID(5);
    }

    private void End()
    {
        dialog.OnDialogCompleted -= End;
        
        Stage1 stage1 = FindObjectOfType<Stage1>();
        if (stage1 == null)       {
            Debug.LogWarning("MrCrow can't find Stage1 using Find object of Type!");
            return;
        }
        stage1.isKeyGet = true;

        AlertManager.Show("已获得道具：打开秘密房间的门禁卡！");
        SaveManager.Instance.Save();
    }
}
