using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/DialogData")]
public class DialogSO : ScriptableObject
{
    public int dialogID;
    public List<DialogLine> DialogLines;
}
