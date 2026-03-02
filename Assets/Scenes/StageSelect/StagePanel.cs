using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StagePanel : MonoBehaviour
{
    [Header("Info")]
    public int stageID;

    [Header("References")]
    public TextMeshProUGUI stageNameText;

    private void Start()
    {
        if (SaveManager.Instance.IsStageUnlocked(stageID))
        {
            stageNameText.text = $"时空 {stageID}：";
            switch (stageID)
            {
                case 1:
                    stageNameText.text += "2110年，香蕉港中文大学废墟";
                    break;
                case 2:
                    stageNameText.text += "2104年，地球某武器研究所";
                    break;
                case 3:
                    stageNameText.text += "2099年，曼波星战舰";
                    break;
            }
        }
        else
        {
            stageNameText.text = $"时空 {stageID}：？？？";
        }
    }
}
