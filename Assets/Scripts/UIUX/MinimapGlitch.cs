using UnityEngine;
using UnityEngine.UI;

public class MinimapGlitch : MonoBehaviour
{
    public RectTransform mapTransform;

    public float glitchStartPercent = 0.7f; // 70%
    public float maxShake = 10f;
    public float glitchSpeed = 20f;

    private RectTransform rect;
    private Vector2 originalPos;
    private Vector3 originalScale;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
        originalScale = rect.localScale;
    }

    void Update()
    {
        if (DarknessSystem.Instance == null) return;

        float darknessPercent = DarknessSystem.Instance.darknessLevel / DarknessSystem.Instance.maxDarkness;

        // If below 70%, reset
        if (darknessPercent < glitchStartPercent)
        {
            rect.anchoredPosition = originalPos;
            rect.localScale = originalScale;
            rect.rotation = Quaternion.identity;
            return;
        }

        // Normalize (0 -> 1 AFTER 70%)
        float t = (darknessPercent - glitchStartPercent) / (1f - glitchStartPercent);

        // SHAKE
        float offsetX = (Mathf.PerlinNoise(Time.time * glitchSpeed, 0) - 0.5f) * maxShake * t;
        float offsetY = (Mathf.PerlinNoise(0, Time.time * glitchSpeed) - 0.5f) * maxShake * t;

        rect.anchoredPosition = originalPos + new Vector2(offsetX, offsetY);

        // SCALE DISTORTION
        float scale = 1f + (t * 0.2f);
        rect.localScale = originalScale * scale;

        // ROTATION DISTORTION
        float rotation = Mathf.Sin(Time.time * glitchSpeed) * 10f * t;
        rect.rotation = Quaternion.Euler(0, 0, rotation);
    }
}

