using UnityEngine;
using UnityEngine.UI;

public class BlurEffect : MonoBehaviour
{
    public float blurAmount = 5f;
    public Material blurMaterial; // Create this in step 3

    void OnEnable()
    {
        if (blurMaterial != null)
        {
            GetComponent<Image>().material = blurMaterial;
            blurMaterial.SetFloat("_BlurAmount", blurAmount);
        }
    }

    void OnDisable()
    {
        GetComponent<Image>().material = null;
    }
}