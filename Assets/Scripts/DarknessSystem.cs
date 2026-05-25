using UnityEngine;

public class DarknessSystem : MonoBehaviour
{

    public static DarknessSystem Instance;

    [Header("Darkness Settings")]
    public float darknessLevel = 0f; // 0 = safe, 100 = danger
    public float maxDarkness = 100f;

    public float increaseRate = 5f;
    public float decreaseRate = 10f;

    [Header("Lighting")]
    public Light directionalLight; // scene light (sun/moon)
    public float minIntensity = 0.2f;
    public float maxIntensity = 1f;

    [Header("Fog Settings")]
    public bool useFog = true;
    public float minFogDensity = 0.001f;
    public float maxFogDensity = 0.05f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        UpdateDarkness();
        UpdateLighting();
    }

    void UpdateDarkness()
    {
        if (LightOrb.Instance == null) return;

        if (LightOrb.Instance.IsLightOn)
        {
            darknessLevel -= decreaseRate * Time.deltaTime;
        }
        else
        {
            darknessLevel += increaseRate * Time.deltaTime;
        }

        darknessLevel = Mathf.Clamp(darknessLevel, 0, maxDarkness);
    }



    void UpdateLighting()
    {
        if (directionalLight != null)
        {
            float t = darknessLevel / maxDarkness;
            directionalLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, t);
        }

       if (useFog)
       {
           float t = darknessLevel / maxDarkness;

           RenderSettings.fog = true;
            RenderSettings.fogDensity = Mathf.Lerp(minFogDensity, maxFogDensity, t);
       }
    }

}

