using UnityEngine;
using TMPro; // ← ADD THIS
using System.Collections;

public class PulseText : MonoBehaviour
{
    private TMP_Text textToPulse; // ← CHANGE TO TMP_Text
    public float pulseSpeed = 1.5f;
    
    void Start()
    {
        textToPulse = GetComponent<TMP_Text>(); // ← CHANGE TO TMP_Text
        
        if (textToPulse == null)
        {
            Debug.LogError("No TMP_Text component found on: " + gameObject.name);
            return;
        }
        
        Debug.Log("PulseText found TMP component: " + textToPulse.name);
        StartCoroutine(PulseEffect());
    }

    IEnumerator PulseEffect()
    {
        Debug.Log("PulseEffect started");
        
        while (true)
        {
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
            Color newColor = Color.Lerp(new Color(1, 1, 1, 0.4f), Color.white, t);
            textToPulse.color = newColor;
            
            yield return null;
        }
    }
}