using UnityEngine;
using UnityEngine.UI;

public class MinimapDistortion : MonoBehaviour
{
   
    public CanvasGroup mapGroup;
    public RectTransform mapTransform;

    void Update()
    {
        float t = UIExposureController.Instance.exposure;

        // Phase 1: fade out first (important)
        mapGroup.alpha = Mathf.Lerp(1f, 0f, UIExposureController.Instance.MidIntensity);

        // Phase 2: if still visible, add instability
        float noise = Mathf.PerlinNoise(Time.time * 2f, 0f) - 0.5f;
        mapTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.2f, t);

        mapTransform.anchoredPosition += new Vector2(noise, -noise) * (t * 10f);
    }
}

