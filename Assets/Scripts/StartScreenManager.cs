using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        
        Debug.Log("StartScreenManager Started");
    }

    void StartGame()
    {
        Debug.Log("Start button clicked!");
        
        // Check if the SceneTransitionManager exists
        if (SceneTransitionManager.instance == null)
        {
            Debug.LogError("SceneTransitionManager instance is NULL!");
            // Fallback: load directly without fade
            UnityEngine.SceneManagement.SceneManager.LoadScene("ModeSelectScene");
            return;
        }
        
        Debug.Log("Calling LoadSceneWithFade...");
        SceneTransitionManager.instance.LoadSceneWithFade("ModeSelectScene");
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}