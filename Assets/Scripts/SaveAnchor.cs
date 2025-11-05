using UnityEngine;

public class SaveAnchor : MonoBehaviour
{
    public int anchorNumber = 0;
    
    private bool _isUnlocked = false;
    public bool isInteractable = true;
    
    private bool playerInRange = false;
    
    public bool IsUnlocked { get { return _isUnlocked; } }
    
    void Start()
    {
        // Load unlock state from save system
        if (SaveManager.instance != null)
        {
            if (SaveManager.instance.currentSave.unlockedAnchorNumbers.Contains(anchorNumber))
            {
                _isUnlocked = true;
            }
        }
        
        // Hide if not interactable OR not unlocked
        GetComponent<SpriteRenderer>().enabled = (isInteractable && _isUnlocked);
        
        Debug.Log($"Anchor {anchorNumber} - Unlocked: {_isUnlocked}, Interactable: {isInteractable}");
    }
    
    void Update()
    {
        if (playerInRange && isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            // ONLY unlock when player presses E, not when they get near
            if (!_isUnlocked)
            {
                UnlockAnchor();
            }
            InteractWithCar();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isInteractable)
        {
            playerInRange = true;
            Debug.Log($"Player near anchor {anchorNumber} - Press E to unlock and interact");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    
    void InteractWithCar()
    {
        Debug.Log($"Anchor {anchorNumber} interacted! Saving as current spawn...");
        
        // SAVE THIS AS THE CURRENT SPAWN NUMBER
        SaveManager.instance.currentSave.currentAnchorNumber = anchorNumber;
        SaveManager.instance.SaveGame();
        
        SceneTransitionManager.instance.LoadSceneWithFade("ModeSelectScene");
    }
    
    public void UnlockAnchor()
    {
        _isUnlocked = true;
        
        // Show the car now that it's unlocked
        if (isInteractable)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        
        // Save to persistent data
        if (SaveManager.instance != null && !SaveManager.instance.currentSave.unlockedAnchorNumbers.Contains(anchorNumber))
        {
            SaveManager.instance.currentSave.unlockedAnchorNumbers.Add(anchorNumber);
            SaveManager.instance.SaveGame();
        }
        
        Debug.Log($"Anchor {anchorNumber} UNLOCKED!");
    }
}