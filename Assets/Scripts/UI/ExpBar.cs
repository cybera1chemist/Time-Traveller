using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Image fillImage;
    public GameObject barRoot;
    public TextMeshProUGUI levelText;


    // This function is called by ExperienceSystem.cs
    public void UpdateExpBar(float ratio, int level)
    { 
        ratio = Mathf.Clamp01(ratio);
        fillImage.fillAmount = ratio;
        levelText.text = $"等级： {level}";
    }
}
