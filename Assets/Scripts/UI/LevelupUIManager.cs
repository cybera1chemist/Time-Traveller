using UnityEngine;

public class LevelUpUIManager : MonoBehaviour
{
    [Header("References")]
    public GameObject levelUpPanel;
    public WeaponDatabase weaponDatabase;
    public LevelupChoicePanel[] choicePanels;

    private PlayerOwn player_own;

    private void Start()
    {
        player_own = FindObjectOfType<PlayerOwn>();
        levelUpPanel.SetActive(false);
    }

    public void Show()
    {
        levelUpPanel.SetActive(true);
        PauseController.Pause(); 
        GenerateOptions();
    }

    public void Close()
    {
        levelUpPanel.SetActive(false);
        PauseController.Resume();
    }

    private void GenerateOptions()
    {
        int num_rewards = choicePanels.Length;
        var options = RewardGenerator.Generate(num_rewards, weaponDatabase.allWeapons.Count, player_own.OwnedWeaponIDs());
        for (int i=0; i<num_rewards; i++)
        {
            choicePanels[i].Setup(options[i], this);
        }
    }
}
