using UnityEngine;
using UnityEngine.UI;

public class ModeSelectManager : MonoBehaviour
{
    public Button storyModeButton;
    public Button bossModeButton;
    public Button waveModeButton;
    public Button cargoModeButton;
    public Button backButton;

    void Start()
    {
        storyModeButton.onClick.AddListener(() => SelectMode("Story"));
        bossModeButton.onClick.AddListener(() => SelectMode("Boss"));
        waveModeButton.onClick.AddListener(() => SelectMode("Wave")); 
        cargoModeButton.onClick.AddListener(() => SelectMode("Cargo"));
        backButton.onClick.AddListener(GoBack);
    }

    void SelectMode(string mode)
    {
        PlayerPrefs.SetString("SelectedMode", mode);
        
        if (mode == "Story")
        {
            SceneTransitionManager.instance.LoadSceneWithFade("LevelSelectScene");
        }
        else
        {
            SceneTransitionManager.instance.LoadSceneWithFade("GameScene");
        }
    }

    void GoBack()
    {
        SceneTransitionManager.instance.LoadSceneWithFade("StartScene");
    }
}