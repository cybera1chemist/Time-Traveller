using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    // public GameObject stagePanel1;
    // public GameObject stagePanel2;

    public void Select_1()
    {
        GlobalSettings.Instance.selectedStage = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level1/Level1");
    }

    public void Select_2()
    {
        GlobalSettings.Instance.selectedStage = 2;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level2/Level2");
    }

    public void Cancel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/CharacterSelect/CharacterSelect");
    }
}
