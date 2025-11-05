using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData currentSave;
    
    private string savePath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Application.persistentDataPath + "/gamesave.save";
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(currentSave, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);
    }
    
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentSave = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded");
        }
        else
        {
            currentSave = new SaveData();
            Debug.Log("New save file created");
        }
    }
}