using UnityEngine;
using UnityEngine.UI;

public class ScreenCorruptionEffect : MonoBehaviour
{
    public CanvasGroup overlay;
    public float flickerSpeed = 20f;

    void Update()
    {
        float t = UIExposureController.Instance.exposure;

        // fade to black in full darkness
        overlay.alpha = Mathf.Lerp(0f, 0.9f, t);

        // flicker like broken signal
        float flicker = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        overlay.alpha *= Mathf.Lerp(0.5f, 1f, flicker * t);
    }
}
