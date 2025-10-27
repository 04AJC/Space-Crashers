using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;

    [Header("UI")]
    public Image healthBar;

    [Header("Physics")]
    public float weight = 1f;
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
            Debug.LogError("EnemyHealth: Rigidbody2D reference is not set!");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void ApplyKnockback(Vector2 sourcePosition, float knockbackPower)
    {
        Debug.Log("ENEMY: Knockback applied! Power: " + knockbackPower + ", Weight: " + weight);
        
        if (rb == null) 
        {
            Debug.LogError("Enemy Rigidbody2D is NULL!");
            return;
        }

        // Calculate direction FROM attacker TO enemy
        Vector2 direction = (rb.position - sourcePosition).normalized;
        
        Debug.Log("Direction before adjustment: " + direction);
        
        // Ensure we always have some upward force for visibility
        direction.y = Mathf.Max(direction.y, 0.3f); // Minimum 0.3 upward
        direction = direction.normalized; // Re-normalize after adjustment
        
        Debug.Log("Direction after adjustment: " + direction);
        
        // Calculate knockback distance - larger multiplier for better visibility
        float knockbackDistance = (knockbackPower / weight) * 1.5f; // Increased from 0.5f
        
        Debug.Log("Knockback distance: " + knockbackDistance);
        
        // Apply the knockback
        Vector2 newPosition = rb.position + (direction * knockbackDistance);
        
        // Respect vertical bounds
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        
        Debug.Log("Moving enemy from: " + rb.position + " to: " + newPosition);
        
        // Move the enemy
        rb.position = newPosition;
        
        Debug.Log("Enemy knockback completed!");
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy Died!");
    }
}