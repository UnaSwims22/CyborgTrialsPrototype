using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    
  
    public static DamageFlash Instance;

    public Image flashImage;
    public float flashDuration = 0.2f;

    private void Awake()
    {
        Instance = this;
    }

    public void Flash()
    {
        if (flashImage == null)
        {
            Debug.LogError("Flash Image is NOT assigned!");
            return;
        }


        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 8f;

            float alpha = Mathf.Sin(t * Mathf.PI); // smooth pulse
            flashImage.color = new Color(1f, 0f, 0f, alpha * 0.6f);

            yield return null;
        }

        flashImage.color = Color.clear;

        // flashImage.color = new Color(1f, 0f, 0f, 0.5f);

        // yield return new WaitForSeconds(flashDuration);

        // flashImage.color = new Color(1f, 0f, 0f, 0f);
    }
}

