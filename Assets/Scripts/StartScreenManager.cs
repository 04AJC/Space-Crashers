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
    }

    void StartGame()
    {
        SceneTransitionManager.instance.LoadSceneWithFade("GameScene");
        
        // Spawn player at car when scene loads
        Invoke(nameof(DelayedSpawn), 0.5f);
    }

    void DelayedSpawn()
    {
        if (PlayerSpawner.instance != null)
        {
            PlayerSpawner.instance.PositionPlayerAtSpawn();
        }
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}