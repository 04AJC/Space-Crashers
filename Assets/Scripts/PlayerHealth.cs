using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 500f;
    public float currentHealth;

    [Header("UI")]
    public Image healthBar;

    [Header("Physics")]
    public float weight = 2f;
    public Rigidbody2D rb;

    [Header("Bounds")]
    public float minY = -4.5f;
    public float maxY = 2.2f;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        
        // Log for debugging
        if (rb == null)
            Debug.LogError("PlayerHealth: Rigidbody2D reference is not set!");
    }

    public void TakeDamage(float amount)
    {
        // NEW: Check if player is invulnerable during dash
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null && playerMovement.isInvulnerable)
        {
            Debug.Log("Damage avoided due to dash invulnerability!");
            return; // Skip damage during i-frames
        }

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Player took " + amount + " damage! Current health: " + currentHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        if (healthBar != null)
        {
            healthBar.fillAmount = fillAmount;
        }
        else
        {
            Debug.LogError("Health Bar reference is null in UpdateHealthBar!");
        }
    }

    public void ApplyKnockback(Vector2 sourcePosition, float knockbackPower)
    {
        Debug.Log("PLAYER: Knockback applied! Power: " + knockbackPower + ", Weight: " + weight);
        
        if (rb == null) 
        {
            Debug.LogError("Player Rigidbody2D is NULL!");
            return;
        }

        // Calculate direction away from attacker
        Vector2 direction = (rb.position - sourcePosition).normalized;
        
        // Add upward component for visual effect
        direction.y = Mathf.Abs(direction.y) + 0.3f;
        direction = direction.normalized;
        
        // Calculate distance based on power and weight (GUARANTEED to move)
        float knockbackDistance = (knockbackPower / weight) * 0.5f;
        
        Debug.Log("Knockback distance: " + knockbackDistance);
        
        // Apply the knockback
        Vector2 newPosition = rb.position + (direction * knockbackDistance);
        
        // Respect vertical bounds
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        
        // Move the player
        rb.position = newPosition;
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player Died!");
    }
}