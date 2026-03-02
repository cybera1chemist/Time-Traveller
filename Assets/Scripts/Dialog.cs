/* 注意：此代码仅用于那种有人物立绘、人物名等的对话！
开场动画用的不是这个！不要搞混了！*/

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Typewriter))]
public class Dialog : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogueRoot;
    public Image portraitImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;

    [Header("Dialogue Content")]
    public DialogSO dialogSO;
    public DialogDB dialogDB;
    public bool hideAtStart = true;

    // Event
    public event Action OnDialogCompleted;

    private int index = 0;
    public bool playing = false;
    private List<DialogLine> lines;
    private Typewriter typewriter;

    private bool dialogCompleted = false;

    private void Start()
    {
        dialogCompleted = false;
        if (hideAtStart)  dialogueRoot.SetActive(false);
        else {
            dialogueRoot.SetActive(true);
            StartDialogue();
        }
    }

    private void Update()
    {
        if (!playing) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            if (!typewriter.IsTyping()) {
                ShowNext();
            }  else {
                typewriter.Skip();
            }
        }
    }

    private void StartDialogue()
    {
        Debug.Log($"对话 {dialogSO.dialogID} 已开始播放。");

        dialogueRoot.SetActive(true);
        lines = dialogSO.DialogLines;
        typewriter = GetComponent<Typewriter>();
        if (dialogSO == null || lines.Count == 0) {
            Debug.LogError("[Dialog] The dialog SO is null or length is 0!!!");
            EndDialogue();
        }

        index = 0;
        playing = true;
        dialogCompleted = false;
        PauseController.Pause();

        ShowLine(lines[index]);
    }

    void ShowNext()
    {
        index++;
        // 是否已经结束
        if (index >= lines.Count)
        {
            EndDialogue();
            return;
        }

        ShowLine(lines[index]);
    }

    void ShowLine(DialogLine line)
    {
        portraitImage.sprite = line.portrait;
        nameText.text = line.speakerName;
        typewriter.Run(line.content, contentText);
    }

    void EndDialogue()
    {
        OnDialogCompleted?.Invoke();
        
        PlayerPrefs.SetInt($"Dialog_ID_{dialogSO.dialogID}_Completed", 1);
        PlayerPrefs.Save();
        
        Debug.Log($"[Dialog] 对话 {dialogSO.dialogID} 已播放完毕。");
        dialogCompleted = true;
        playing = false;
        dialogueRoot.SetActive(false);
        PauseController.Resume();
    }

    #region public APIs
    public void PlayDialogData(DialogSO data) {
        dialogueRoot.SetActive(true);
        dialogSO = data;
        StartDialogue();
    }
    public void PlayDialogID(int id)
    {
        dialogueRoot.SetActive(true);
        dialogSO = dialogDB.GetDialog(id);
        if (dialogSO == null)
        {
            Debug.LogWarning("[Dialog] dialog SO of id " + id + " not found in DB!");
        }
        StartDialogue();
    }
    public void SetDialogID(int id)
    {
        dialogSO = dialogDB.GetDialog(id);
    }

    public bool IsPlayingID(int id) => playing && dialogSO.dialogID == id;

    public bool IsDialogCompleted() => dialogCompleted;

    #endregion
}

[Serializable]
public class DialogLine
{
    public Sprite portrait;
    public string speakerName;
    [TextArea(2, 4)]
    public string content;
}

