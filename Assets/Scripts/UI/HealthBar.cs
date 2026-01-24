using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;       // 填充条
    public GameObject barRoot;    // 整个血条对象
    public CanvasGroup canvasGroup; // 控制可见性但保持对象启用
    public GameObject entity;

    private Health health;

    private void Start()
    {
        if (entity != null)
            health = entity.GetComponent<Health>();
        else
            Debug.LogWarning("HealthBar.entity is not assigned.", this);

        if (barRoot != null)
        {
            canvasGroup = barRoot.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = barRoot.AddComponent<CanvasGroup>();
        }
        else
        {
            Debug.LogWarning("HealthBar.barRoot is not assigned.", this);
        }
    }
    private void Update()
    {
        if (health == null || fillImage == null || canvasGroup == null)
            return;

        float ratio = health.currentHealth / health.maxHealth;
        ratio = Mathf.Clamp01(ratio);

        // 更新血条数值
        fillImage.fillAmount = ratio;

        // 控制显示/隐藏（只改变可见性，不禁用对象或脚本）
        bool show = ratio < 1f;
        canvasGroup.alpha = show ? 1f : 0f;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }
}
