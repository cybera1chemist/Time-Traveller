using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HaizaoUnlock : MonoBehaviour
{
    private Dialog dialog;
    private GameStatsManager manager;

    void Start()
    {
        if (SaveManager.Instance.IsCharacterUnlocked(4))
        {
            gameObject.SetActive(false);
            return;
        }
        
        manager = FindObjectOfType<GameStatsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("[HaizaoUnlock] 主角团遇到了海藻。");
            PlayDialog();
        }
    }


    private void PlayDialog()
    {
        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        dialog.OnDialogCompleted += End;

        if (dialog == null)
        {
            Debug.LogWarning("[HaizaoUnlock] Can't find dialog using Find object of Type!");
        }

        dialog.PlayDialogID(6);
    }

    private void End()
    {
        dialog.OnDialogCompleted -= End;
        
        SaveManager.Instance.UnlockCharacter(4);
        AlertManager.Show("已解锁新角色海藻！\n在新的一局游戏中，可在角色选择界面查看。");
        SaveManager.Instance.Save();

        gameObject.SetActive(false);
    }
}
