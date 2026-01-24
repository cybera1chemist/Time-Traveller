using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Database/CharacterDatabase")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterData> allCharacters = new();

    #region 处理缓存
    private Dictionary<int, CharacterData> cache;

    private void InitCache()
    {
        cache = new Dictionary<int, CharacterData>();
        foreach (var c in allCharacters)
        {
            if (c == null) continue;
            if (cache.ContainsKey(c.ID)) continue;
            cache[c.ID] = c;
        }
    }
    #endregion

    #region 获取信息用的API
    // character data 储存关于角色的全部信息，包括立绘等；
    // 而 character stats 只储存和战斗有关的信息，例如攻击力等。
    // characterData里面就包含了characterStats。
    public CharacterData GetCharacterDataByID(int id)
    {
        if (cache == null) InitCache();
        if (!cache.TryGetValue(id, out CharacterData data))
        {
            Debug.LogError($"[CharacterDatabase] Get character ID {id} failed.");
            return null;
        }
        return data;
    }

    public CharacterStats GetCharacterStatsByID(int id)
    {
        return GetCharacterDataByID(id).stats;
    }
    #endregion
}
