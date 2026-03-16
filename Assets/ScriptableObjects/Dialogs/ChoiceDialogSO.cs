using UnityEngine;

[CreateAssetMenu(menuName = "Game/ChoiceDialogData")]
public class ChoiceDialogSO : DialogSO
{
    [Header("Choices")]
    public string optionAText;
    public DialogSO outcomeA; // What happens if A is picked
    // Alternatively, you could use an ID if you prefer: public int outcomeA_ID;

    public string optionBText;
    public DialogSO outcomeB;
}
