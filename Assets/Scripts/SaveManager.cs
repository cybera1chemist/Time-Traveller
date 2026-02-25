using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSave();
    }

    private void InitializeSave()
    {
        #if UNITY_EDITOR
            // 为方便测试，每次都用全新的存档
            PlayerPrefs.DeleteAll();
        #endif

        if (!PlayerPrefs.HasKey("Inited"))
        {
            PlayerPrefs.SetInt("Inited", 1);

            // 角色解锁进度
            PlayerPrefs.SetInt("Character_1_Unlocked", 1);
            PlayerPrefs.SetInt("Character_2_Unlocked", 0);
            PlayerPrefs.SetInt("Character_3_Unlocked", 0);
            PlayerPrefs.SetInt("Character_4_Unlocked", 0);

            // 剧情进度
            PlayerPrefs.SetInt("IntroAnim_Completed", 0);
            PlayerPrefs.SetInt("Stage1_Dialog1_Completed", 0);

            // 关卡解锁进度
            PlayerPrefs.SetInt("Stage1_Unlocked", 1);
            PlayerPrefs.SetInt("Stage2_Unlocked", 0);
            PlayerPrefs.SetInt("Stage3_Unlocked", 0);

            PlayerPrefs.Save();
        }
    }

    #region public API
    // Character
    public bool IsCharacterUnlocked(int characterID)
    {
        return PlayerPrefs.GetInt($"Character_{characterID}_Unlocked", 0) == 1;
    }
    public void UnlockCharacter(int characterID)
    {
        PlayerPrefs.SetInt($"Character_{characterID}_Unlocked", 1);
        PlayerPrefs.Save();
    }

    // Levels
    public bool IsStageUnlocked(int stageID)
    {
        return PlayerPrefs.GetInt($"Stage{stageID}_Unlocked", 0) == 1;
    }

    public void UnlockStage(int stageID)
    {
        PlayerPrefs.SetInt($"Stage{stageID}_Unlocked", 1);
        PlayerPrefs.Save();
    }

    public void Save() => PlayerPrefs.Save();

    #endregion
}
