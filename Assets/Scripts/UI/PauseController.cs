using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    public static void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        if (GameStatsManager.Instance != null)
            GameStatsManager.Instance.StopCounting();
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        if (GameStatsManager.Instance != null)
            GameStatsManager.Instance.StartCounting();
    }
}
