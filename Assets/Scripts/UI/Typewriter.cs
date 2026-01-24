using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Typewriter : MonoBehaviour
{
    public float Speed = 15;
    public string textContent;

    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private TextMeshProUGUI currentLabel;
    private string currentText;

    public void Run(string textToType, TextMeshProUGUI textLabel)
    {
        // 如果之前有协程，先停掉
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        currentText = textToType;
        currentLabel = textLabel;

        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    IEnumerator TypeText(string textToType, TextMeshProUGUI textLabel)
    {
        float t = 0;  //经过的时间
        int charIndex = 0;
        isTyping = true;
        while (charIndex < textToType.Length)
        {
            t += Time.unscaledDeltaTime * Speed;   //简单计时器赋值给t
            charIndex = Mathf.FloorToInt(t);       //把t转为int类型赋值给charIndex
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            textLabel.text = textToType[..charIndex];

            yield return null;
        }
        isTyping = false;
        textLabel.text = textToType;
        typingCoroutine = null;
    }

    public bool IsTyping() => isTyping;

    // 新增：跳过打字，直接显示完整内容
    public void Skip()
    {
        if (!isTyping) return;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        if (currentLabel != null)
        {
            currentLabel.text = currentText;
        }

        isTyping = false;
    }
}