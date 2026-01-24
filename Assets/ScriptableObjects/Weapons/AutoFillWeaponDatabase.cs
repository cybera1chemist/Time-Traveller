/*
The purpose of this script is to auto-fill the 
WeaponDatabase ScriptableObject with all WeaponData assets
found in the project. 
*/

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;

public static class AutoFillWeaponDatabaseEditor
{
    [MenuItem("Tools/Weapons/AutoFill Database")]
    public static void AutoFill()
    {
        string[] guids = AssetDatabase.FindAssets("t:WeaponData");
        var dbs = AssetDatabase.FindAssets("t:WeaponDatabase");
        if (dbs.Length == 0)
        {
            Debug.LogError("No WeaponDatabase asset found. Create one first.");
            return;
        }

        string dbPath = AssetDatabase.GUIDToAssetPath(dbs[0]);
        var db = AssetDatabase.LoadAssetAtPath<WeaponDatabase>(dbPath);
        db.allWeapons = guids.Select(g => AssetDatabase.LoadAssetAtPath<WeaponData>(AssetDatabase.GUIDToAssetPath(g))).ToList();
        EditorUtility.SetDirty(db);
        AssetDatabase.SaveAssets();
        Debug.Log($"WeaponDatabase auto-filled with {db.allWeapons.Count} WeaponData assets.");
    }
}
#endif
