using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    // NEW: Anchor system for car saves
    public string lastUsedAnchorID = "Fields_Start";
    public List<string> unlockedAnchors = new List<string>() { "Fields_Start" };
    
    // Your existing progression (add these if you don't have them yet)
    public int highestLevelUnlocked = 1;
    public List<int> completedLevels = new List<int>();
    
    // Game mode scores
    public int bossModeHighScore = 0;
    public int waveModeHighScore = 0;
    public int transitModeHighScore = 0;
    public int cargoModeHighScore = 0;
}
