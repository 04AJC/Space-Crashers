using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    [Header("Dash")]
    public float dashDistance = 3f;
    public float dashCooldown = 1.5f;
    public float dashDuration = 0.2f;
    private float lastDashTime = -999f;
    private bool isDashing = false;
    private Vector2 dashDirection;

    [Header("Invulnerability")]
    public bool isInvulnerable = false;
    public float invulnerabilityFlashRate = 0.1f;

    [Header("UI")]
    public Image dashCooldownImage;

    // Vertical bounds
    private float minY = -4.5f;
    private float maxY = 2.2f;

    // Reference to PlayerHealth for invulnerability
    private PlayerHealth playerHealth;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Flip sprite
        if (movement.x > 0)
            spriteRenderer.flipX = true;
        else if (movement.x < 0)
            spriteRenderer.flipX = false;

        // Dash input
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space)) && !isDashing)
        {
            TryDash();
        }

        UpdateDashCooldownUI();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // During dash, use dash physics
            rb.linearVelocity = dashDirection * (dashDistance / dashDuration);
        }
        else
        {
            // FIXED: Include Y movement in normal movement
            Vector2 velocity = movement * moveSpeed; // This includes both X and Y
            rb.linearVelocity = velocity;
        }

        // Clamp vertical position (prevents leaving screen)
        Vector2 currentPosition = rb.position;
        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
        rb.position = currentPosition;
    }

    void TryDash()
    {
        if (Time.time - lastDashTime < dashCooldown)
            return;

        dashDirection = GetDashDirection();
        StartDash();
    }

    Vector2 GetDashDirection()
    {
        if (movement != Vector2.zero)
        {
            return movement.normalized;
        }
        else
        {
            return spriteRenderer.flipX ? Vector2.right : Vector2.left;
        }
    }

    void StartDash()
    {
        isDashing = true;
        isInvulnerable = true;
        lastDashTime = Time.time;
        
        StartCoroutine(FlashDuringDash());
        
        Invoke(nameof(EndDash), dashDuration);
    }

    void EndDash()
    {
        isDashing = false;
        isInvulnerable = false;
        
        Color normalColor = spriteRenderer.color;
        normalColor.a = 1f;
        spriteRenderer.color = normalColor;
        
        if (movement == Vector2.zero)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    System.Collections.IEnumerator FlashDuringDash()
    {
        float endTime = Time.time + dashDuration;
        
        while (Time.time < endTime && isDashing)
        {
            Color flashColor = spriteRenderer.color;
            flashColor.a = flashColor.a == 1f ? 0.3f : 1f;
            spriteRenderer.color = flashColor;
            
            yield return new WaitForSecondsRealtime(invulnerabilityFlashRate); // Use Realtime since timescale might be 0
        }
        
        Color finalColor = spriteRenderer.color;
        finalColor.a = 1f;
        spriteRenderer.color = finalColor;
    }

    void UpdateDashCooldownUI()
    {
        if (dashCooldownImage != null)
        {
            float cooldownProgress = (Time.time - lastDashTime) / dashCooldown;
            dashCooldownImage.fillAmount = Mathf.Clamp01(cooldownProgress);
        }
    }

    public bool CanDash()
    {
        return Time.time - lastDashTime >= dashCooldown;
    }

    public float GetDashCooldownProgress()
    {
        return Mathf.Clamp01((Time.time - lastDashTime) / dashCooldown);
    }
}