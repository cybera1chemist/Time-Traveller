using UnityEngine;

public class Goldfish : MonoBehaviour
{
    public int dialogID = 202; // 金鱼老师的对话ID

    private Dialog dialog;
    private GameStatsManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameStatsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("主角团遇到了金鱼老师。");
            PlayDialog();
        }
    }

    private void PlayDialog()
    {
        // Find dialog panel
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        dialog.OnDialogCompleted += End;

        dialog.PlayDialogID(dialogID);
    }

    private void End()
    {
        dialog.OnDialogCompleted -= End;

        gameObject.SetActive(false);
    }
}
