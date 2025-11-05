using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PositionPlayerAtSpawn()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "GameScene")
            return;
        
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;
        
        if (SaveManager.instance == null) return;
        
        int targetAnchorNumber = SaveManager.instance.currentSave.currentAnchorNumber;
        
        // ONLY spawn at anchor if it's NOT the default (0) OR if player has actually unlocked other anchors
        if (targetAnchorNumber != 0 || SaveManager.instance.currentSave.unlockedAnchorNumbers.Count > 1)
        {
            SaveAnchor spawnAnchor = FindAnchorByNumber(targetAnchorNumber);
            
            if (spawnAnchor != null && spawnAnchor.IsUnlocked)
            {
                player.transform.position = spawnAnchor.transform.position;
                Debug.Log($"Player spawned at anchor {targetAnchorNumber}");
                return;
            }
        }
        
        Debug.Log("Player using default spawn position (first play or no valid anchor)");
        // Player stays at their editor position - PERFECT for first play
    }
    
    SaveAnchor FindAnchorByNumber(int anchorNumber)
    {
        SaveAnchor[] allAnchors = FindObjectsByType<SaveAnchor>(FindObjectsSortMode.None);
        
        foreach (SaveAnchor anchor in allAnchors)
        {
            if (anchor.anchorNumber == anchorNumber)
            {
                return anchor;
            }
        }
        
        return null;
    }
}