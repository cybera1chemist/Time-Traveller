using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Typewriter))]
public class IntroManager : MonoBehaviour
{
    public IntroSlide[] slides;
    public Image imageHolder;         
    public TextMeshProUGUI textHolder; 
    public string mainMenuScenePath = "MainMenu";
    
    private Typewriter typewriter;

    private int currentIndex = 0;
    private bool isTyping = false;

    private void Awake()
    {
        // 如果看过开场剧情了，就不放了
        if (PlayerPrefs.GetInt("IntroAnim_Completed", 0) == 1)
        {
            TryLoadMainMenu();
            return;
        }
    }

    void Start()
    {
        typewriter = GetComponent<Typewriter>();
        ShowSlide();
    }

    void Update()
    {
        isTyping = typewriter.IsTyping();
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("[IntroManager] Input detected in Update(). isTyping = " + isTyping);
            if (!isTyping) ShowSlide();
            else SkipSlide();
        }
    }

    private void ShowSlide()
    {
        if (currentIndex >= slides.Length)
        {
            TryLoadMainMenu();
            return;
        }

        imageHolder.sprite = slides[currentIndex].image;
        typewriter.Run(slides[currentIndex].description, textHolder);

        currentIndex++;
    }

    private void SkipSlide()
    {
        typewriter.Skip();
    }

    void TryLoadMainMenu()
    {
        int index = SceneUtility.GetBuildIndexByScenePath(mainMenuScenePath);
        if (index != -1)
        {
            PlayerPrefs.SetInt("IntroAnim_Completed", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(index);
            return;
        } else
        {
            Debug.LogError("[IntroManager] Cannot load MainMenu scene! Check the scene path in IntroManager.");
        }
    }
}
