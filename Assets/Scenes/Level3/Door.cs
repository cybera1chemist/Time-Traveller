using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Before Unlocked")]
    [SerializeField] private string lockedAlertMessage;

    [Header("How to Unlock")]
    [SerializeField] private string unlockCondition;

    [Header("After Unlocked")]
    [SerializeField] private bool playDialogOnEnter;
    [SerializeField] private int dialogID;
    [SerializeField] private bool disappearAfterDialog = false;
    private bool dialogPlayed = false;

    private Dialog dialog;
    private GameStatsManager manager;


    private bool isLocked = true;

    private void Start()
    {
        manager = FindObjectOfType<GameStatsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Compare whether unlocked
            if (PlayerPrefs.GetInt(unlockCondition, 0) == 1)  isLocked = false;

            if (isLocked)
            {
                AlertManager.Show(lockedAlertMessage);
            } else
            {
                if (!dialogPlayed && playDialogOnEnter)
                {
                    PlayDialog();
                    dialogPlayed = true;
                }
            }
        }
    }

    private void PlayDialog()
    {
        manager.ActivateDialogPanel();
        dialog = FindObjectOfType<Dialog>();
        dialog.PlayDialogID(dialogID);
        dialog.OnDialogCompleted += End;
    }

    private void End()
    {   
        dialog.OnDialogCompleted -= End;
        if (disappearAfterDialog) gameObject.SetActive(false);
    }
}
