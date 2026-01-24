using System;

[Serializable]
public class SpawnEvent
{
    public float startMinute;
    public float endMinute;

    public int enemyId = 1;

    public float spawnInterval = 1f;   // 每隔多久生成一次
    public int spawnCount = 1;         // 每次生成几个

    public bool spawnOnce = false; // Boss 用的
}
