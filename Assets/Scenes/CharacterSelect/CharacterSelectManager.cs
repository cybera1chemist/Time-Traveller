using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public TextMeshProUGUI viewPanelText;
    [Header("Buttons")]
    [SerializeField] private Button viewButton2;
    [SerializeField] private Button viewButton3;
    [SerializeField] private Button viewButton4;

    [Header("Image Holders")]
    [SerializeField] private Image imageHolder2;
    [SerializeField] private Image imageHolder3;
    [SerializeField] private Image imageHolder4;

    [SerializeField] private Sprite unknownCharacterImage;

    [SerializeField] private int selectedPlayerID;


    private void Start() {
        viewPanelText.text = "";
        selectedPlayerID = -1;
        if (!SaveManager.Instance.IsCharacterUnlocked(2)) imageHolder2.sprite = unknownCharacterImage;
        if (!SaveManager.Instance.IsCharacterUnlocked(3)) imageHolder3.sprite = unknownCharacterImage;
        if (!SaveManager.Instance.IsCharacterUnlocked(4)) imageHolder4.sprite = unknownCharacterImage;
    }

    public void ShowInfo1()
    {
        viewPanelText.text = "李嘟嘟，香蕉港中文大学的时间信息工程专业学生，擅长使用科技类武器。";
        selectedPlayerID = 1;
        GlobalSettings.Instance.selectedCharacterID = selectedPlayerID;
    }

    public void ShowInfo2()
    {
        if (!SaveManager.Instance.IsCharacterUnlocked(2))
        {
            viewPanelText.text = "角色未解锁！完成特定任务以解锁该角色。";
            return;
        }
        viewPanelText.text = "诸葛好帅，香蕉港中文大学的中国古典法术专业学生，擅长使用法术类武器。";
        selectedPlayerID = 2;
        GlobalSettings.Instance.selectedCharacterID = selectedPlayerID;
    }

    public void ShowInfo3()
    {
        if (!SaveManager.Instance.IsCharacterUnlocked(3))
        {
            viewPanelText.text = "角色未解锁！完成特定任务以解锁该角色。";
            return;
        }
        viewPanelText.text = "谜奥，香蕉港中文大学的量子物理专业学生，来自曼波星球的神秘交流生。";
        selectedPlayerID = 3;
        GlobalSettings.Instance.selectedCharacterID = selectedPlayerID;
    }

    public void ShowInfo4()
    {
        if (!SaveManager.Instance.IsCharacterUnlocked(4))
        {
            viewPanelText.text = "角色未解锁！完成特定任务以解锁该角色。";
            return;
        }

        viewPanelText.text = "海藻，香蕉港中文大学虚拟现实游戏设计课程的人工智能助教。";
        selectedPlayerID = 4;
        GlobalSettings.Instance.selectedCharacterID = selectedPlayerID;
    }
    

    public void OnClickSelect()
    {
        if (selectedPlayerID <= 0)
        {
            viewPanelText.text = "请先选择一个角色！";
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/StageSelect/StageSelect");
    }
}
