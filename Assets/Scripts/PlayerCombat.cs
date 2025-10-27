using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 1f;
    public int attackDamage = 10;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    [Header("Layers")]
    public LayerMask enemyLayers;

    [Header("Knockback")]
    public float knockbackPower = 5f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        Debug.Log("=== PLAYER ATTACK STARTED ===");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
        Debug.Log("Found " + hitEnemies.Length + " enemies in attack range");

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Processing enemy: " + enemy.name);
            
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("EnemyHealth found! Applying damage and knockback...");
                enemyHealth.TakeDamage(attackDamage);
                
                // Apply knockback to enemy
                Debug.Log("Calling ApplyKnockback with power: " + knockbackPower);
                enemyHealth.ApplyKnockback(transform.position, knockbackPower);
                
                DamagePopup.Create(enemy.transform.position, attackDamage);
            }
            else
            {
                Debug.LogError("No EnemyHealth component found on: " + enemy.name);
            }
        }
        Debug.Log("=== PLAYER ATTACK COMPLETED ===");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}