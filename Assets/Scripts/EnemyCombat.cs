using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackRange = 1f;
    public int attackDamage = 5;
    public float attackRate = 1f;
    
    [Header("Knockback")]
    public float knockbackPower = 30f;

    private float nextAttackTime = 0f;
    private Transform player;
    private PlayerHealth playerHealth;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        
        Debug.Log("EnemyCombat started. Knockback power: " + knockbackPower);
    }

    void Update()
    {
        if (player != null && playerHealth != null && Time.time >= nextAttackTime)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= attackRange)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        if (playerHealth != null)
        {
            Debug.Log("Enemy attacking player!");
            
            // Store damage amount for popup
            int damageAmount = attackDamage;
            
            // Apply knockback first (visual effect still happens)
            playerHealth.ApplyKnockback(transform.position, knockbackPower);
            
            // Then try to apply damage - this will return if player is invulnerable
            playerHealth.TakeDamage(damageAmount);
            
            // Only show popup if damage was actually taken
            // We need to check if player is still alive and not invulnerable
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null && !playerMovement.isInvulnerable)
            {
                DamagePopup.Create(player.position, damageAmount);
            }
            else
            {
                Debug.Log("No damage popup - player is invulnerable!");
            }
        }
        else
        {
            Debug.LogError("PlayerHealth is null in EnemyCombat!");
        }
    }
}