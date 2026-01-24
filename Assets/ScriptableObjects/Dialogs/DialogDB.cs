using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/DialogDatabase")]
public class DialogDB : ScriptableObject
{
    public List<DialogSO> dialogDataList;

    private Dictionary<int, DialogSO> lookup;

    private void OnEnable()
    {
        lookup = new Dictionary<int, DialogSO>();

        foreach (var d in dialogDataList)
        {
            if (!lookup.ContainsKey(d.dialogID))
                lookup.Add(d.dialogID, d);
        }
    }

    public DialogSO GetDialog(int id)
    {
        if (lookup.ContainsKey(id))
            return lookup[id];
        
        Debug.LogWarning("Dialog id not found: " + id);
        return null;
    }
}
