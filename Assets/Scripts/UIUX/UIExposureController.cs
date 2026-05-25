using UnityEngine;

public class UIExposureController : MonoBehaviour
{
    public static UIExposureController Instance;

    [Range(0f, 1f)]
    public float exposure; // 0 = safe, 1 = full darkness

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (DarknessSystem.Instance == null) return;

        exposure = DarknessSystem.Instance.darknessLevel / DarknessSystem.Instance.maxDarkness;
    }

    // helper curves for better control
    public float LowIntensity => Mathf.Clamp01(exposure / 0.4f);
    public float MidIntensity => Mathf.Clamp01((exposure - 0.3f) / 0.4f);
    public float HighIntensity => Mathf.Clamp01((exposure - 0.7f) / 0.3f);
}
