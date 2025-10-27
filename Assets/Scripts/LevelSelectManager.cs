using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button backButton;

    void Start()
    {
        level1Button.onClick.AddListener(() => LoadLevel(1));
        level2Button.onClick.AddListener(() => LoadLevel(2));
        level3Button.onClick.AddListener(() => LoadLevel(3));
        backButton.onClick.AddListener(GoBack);
    }

    void LoadLevel(int levelNumber)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        SceneTransitionManager.instance.LoadSceneWithFade("GameScene");
    }

    void GoBack()
    {
        SceneTransitionManager.instance.LoadSceneWithFade("ModeSelectScene");
    }
}