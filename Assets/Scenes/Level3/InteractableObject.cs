using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private int dialogID;
    [SerializeField] private bool dontRepeatDialog = true;
    [SerializeField] private bool disappearAfterDialog = true;

    private Dialog dialog;
    private GameStatsManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameStatsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("[InteractableObject] 玩家触发了一个可交互对象。");
            PlayDialog();
        }
    }


    private void PlayDialog()
    {
        if (dontRepeatDialog)
        {
           if (SaveManager.Instance.IsDialogCompleted(dialogID))
           {
               Debug.Log("[InteractableObject] 对话 " + dialogID + " 已经看过了，重复触发但不播放。");
               return;
           }
        }

        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        dialog.OnDialogCompleted += End;

        if (dialog == null)
        {
            Debug.LogWarning("[InteractableObject] Can't find dialog using Find object of Type!");
        }

        dialog.PlayDialogID(dialogID);
    }

    private void End()
    {
        dialog.OnDialogCompleted -= End;
        
        SaveManager.Instance.Save();

        if (disappearAfterDialog)  gameObject.SetActive(false);
    }
}
