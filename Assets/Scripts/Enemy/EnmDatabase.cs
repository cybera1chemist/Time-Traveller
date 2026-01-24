using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyData> enemies;

    private Dictionary<int, EnemyData> lookup;

    private void OnEnable()
    {
        lookup = new Dictionary<int, EnemyData>();

        foreach (var e in enemies)
        {
            if (!lookup.ContainsKey(e.id))
                lookup.Add(e.id, e);
        }
    }

    public EnemyData GetEnemy(int id)
    {
        if (lookup.ContainsKey(id))
            return lookup[id];
        
        Debug.LogWarning("Enemy id not found: " + id);
        return null;
    }
}
