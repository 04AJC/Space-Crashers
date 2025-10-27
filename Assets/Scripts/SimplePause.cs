using UnityEngine;
using UnityEngine.UI;

public class SimplePause : MonoBehaviour
{
    public GameObject pausePanel;
    public Image blurOverlay;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
                Resume();
            else
                Pause();
        }
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        if (pausePanel != null) pausePanel.SetActive(true);
        if (blurOverlay != null) blurOverlay.gameObject.SetActive(true);
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
        if (pausePanel != null) pausePanel.SetActive(false);
        if (blurOverlay != null) blurOverlay.gameObject.SetActive(false);
    }
    
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
