using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    void Start()
    {
        // Spawn player at car when GameScene loads (including when returning from menu)
        if (PlayerSpawner.instance != null)
        {
            PlayerSpawner.instance.PositionPlayerAtSpawn();
        }
    }
}
