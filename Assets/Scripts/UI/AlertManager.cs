/*
注意：Alert.cs 要挂在UI Manager object（例如canvas上），
    而不是 Alert prefab 上面！
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Instance;

    [Header("References")]
    [SerializeField] private AlertUI alertPrefab;
    [SerializeField] private Transform alertParent;

    private GameObject alertObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void SpawnAlert(string msg)
    {
        alertObject = Instantiate(alertPrefab.gameObject, alertParent);
        alertObject.GetComponent<AlertUI>().SetText(msg);
        PauseController.Pause();
        // TODO: 添加提示弹出时的音效！
    }

    #region public API
    public static void Show(string msg)
    {
        Instance.SpawnAlert(msg);
    }
    #endregion
}
