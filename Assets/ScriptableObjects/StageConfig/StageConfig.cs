using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/StageConfig")]
public class StageConfig : ScriptableObject
{
    public List<SpawnEvent> spawnEvents;
}
