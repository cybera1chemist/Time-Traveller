using TMPro;
using UnityEngine;

public class AlertUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSource sfxPlayer;

    private void Start()
    {
        PauseController.Pause();
        sfxPlayer.Play();
    }

    public void SetText(string msg)
    {
        Debug.Log($"[AlertUI] Alert: {msg}");
        text.text = msg;
    }

    public void Close()
    {
        Debug.Log("[AlertUI] The alert is closed.");
        PauseController.Resume();
        Destroy(gameObject);
    }
}
