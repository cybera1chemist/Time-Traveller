using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageInfoText;

    private void Start()
    {
        GlobalSettings.Instance.selectedStage = 0;
    }

    #region 按按钮
    public void Select_1()
    {
        GlobalSettings.Instance.selectedStage = 1;
        UpdateStageInfoText();
    }

    public void Select_2()
    {
        if (SaveManager.Instance.IsStageUnlocked(2)){
            GlobalSettings.Instance.selectedStage = 2;
            UpdateStageInfoText();
        } else
        {
            AlertManager.Show("无法前往此时空……");
        }
    }

    public void Select_3()
    {
        if (SaveManager.Instance.IsStageUnlocked(3))
        {
            GlobalSettings.Instance.selectedStage = 3;
            UpdateStageInfoText();
        } else
        {
            AlertManager.Show("无法前往此时空……"); 
        }
    }

    public void Confirm()
    {
        if (GlobalSettings.Instance.selectedStage == 0)
        {
            AlertManager.Show("请先选择要穿越的时空");
            return;
        }
        SceneManager.LoadScene($"Scenes/Level{GlobalSettings.Instance.selectedStage}/Level{GlobalSettings.Instance.selectedStage}");
    }

    public void Cancel()
    {
        SceneManager.LoadScene("Scenes/CharacterSelect/CharacterSelect");
    }
    #endregion

    #region 更新目前可公开的情报
    private void UpdateStageInfoText()
    {
        switch (GlobalSettings.Instance.selectedStage)
        {
            case 0:
                stageInfoText.text = "";
                break;
            case 1:
                UpdateStage1Info();
                break;
            case 2:

                break;
            case 3:

                break;
        }
    }

    private void UpdateStage1Info()
    {
        stageInfoText.text = "";
        // if (SaveManager.Instance.)
    }

    #endregion
}
