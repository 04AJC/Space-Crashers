using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // CRITICAL: Start with transparent fade image
            if (fadeImage != null)
            {
                Color color = fadeImage.color;
                color.a = 0f; // Set completely transparent
                fadeImage.color = color;
                fadeImage.raycastTarget = false; // Don't block mouse clicks
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }
    
    IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Enable raycast target during transition to block clicks
        fadeImage.raycastTarget = true;
        
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));
        
        // Load the new scene
        SceneManager.LoadScene(sceneName);
        
        // Fade back in
        yield return StartCoroutine(Fade(1f, 0f));
        
        // Disable raycast target after fade-in
        fadeImage.raycastTarget = false;
    }
    
    IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        float timer = 0f;
        Color color = fadeImage.color;
        color.a = startAlpha;
        fadeImage.color = color;
        
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float blend = Mathf.Clamp01(timer / fadeDuration);
            color.a = Mathf.Lerp(startAlpha, targetAlpha, blend);
            fadeImage.color = color;
            yield return null;
        }
        
        color.a = targetAlpha;
        fadeImage.color = color;
    }
    
    // Optional: Call this if you need to manually reset the fade
    public void ResetFade()
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
            fadeImage.raycastTarget = false;
        }
    }
}