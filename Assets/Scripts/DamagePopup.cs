using UnityEngine;
using TMPro;  // Import TextMeshPro

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float moveSpeed = 2f;
    public float fadeSpeed = 2f;

    public static void Create(Vector3 position, int damageAmount)
    {
        // Load prefab from Resources
        GameObject popupPrefab = Resources.Load<GameObject>("DamagePopupPrefab");
        
        // CHANGE: Find the Canvas in the scene
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        
        // CHANGE: Instantiate the popup as a child of the Canvas
        GameObject popup = Instantiate(popupPrefab, canvas.transform);

        // Convert world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        popup.transform.position = screenPos;

        popup.GetComponent<DamagePopup>().Setup(damageAmount);
    }

    public void Setup(int damageAmount)
    {
        text.text = damageAmount.ToString();
    }

    void Update()
    {
        // Move upward (in screen space)
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Fade out
        Color color = text.color;
        color.a -= fadeSpeed * Time.deltaTime;
        text.color = color;

        // Destroy after invisible
        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}