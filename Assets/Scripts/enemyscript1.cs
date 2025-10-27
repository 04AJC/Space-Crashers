using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float chaseTime = 20f; // NEW: How long to chase after losing detection
    
    private Transform player;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    
    // NEW: Chase timer variables
    private float chaseTimer = 0f;
    private bool isChasing = false;

    // Vertical bounds
    private float minY = -4.5f;
    private float maxY = 2.2f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            // NEW: Update chase timer
            if (isChasing)
            {
                chaseTimer -= Time.deltaTime;
                if (chaseTimer <= 0f)
                {
                    isChasing = false;
                    Debug.Log("Enemy lost interest in player");
                }
            }
            
            // NEW: Start chasing if player is in detection range OR if already chasing
            if (distanceToPlayer <= detectionRange || isChasing)
            {
                if (!isChasing)
                {
                    isChasing = true;
                    chaseTimer = chaseTime;
                    Debug.Log("Enemy started chasing player!");
                }

                // Flip sprite based on player position
                if (player.position.x > transform.position.x)
                {
                    spriteRenderer.flipX = true;
                }
                else if (player.position.x < transform.position.x)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (player != null && isChasing) // NEW: Only move if chasing
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            if (distanceToPlayer > attackRange)
            {
                // Use physics for movement
                Vector2 direction = ((Vector2)player.position - rb.position).normalized;
                rb.linearVelocity = direction * moveSpeed;
            }
            else
            {
                // Stop moving when in attack range
                rb.linearVelocity = Vector2.zero;
            }

            // Clamp Y position
            Vector2 currentPosition = rb.position;
            currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
            rb.position = currentPosition;
        }
        else
        {
            // NEW: Stop moving if not chasing
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}