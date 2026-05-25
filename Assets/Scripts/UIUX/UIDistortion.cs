using UnityEngine;
using UnityEngine.UI;

public class UIDistortion : MonoBehaviour
{
   
    public float maxShake = 30f;
    public float distortionSpeed = 5f;
    public float maxScaleEffect = 0.05f;
    public float maxRotationEffect = 2f;


    private RectTransform rect;
    private Vector2 originalPos;
    private Vector3 originalScale;
    private Quaternion originalRotation;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
        originalScale = rect.localScale;
        originalRotation = rect.localRotation;

    }

    void Update()
    {
        
        
        if (DarknessSystem.Instance == null) return;

        float t = DarknessSystem.Instance.GetNormalizedDarknessLevel();
            //darknessLevel / DarknessSystem.Instance.maxDarkness;

        float noiseX = Mathf.PerlinNoise(Time.time * distortionSpeed, 0f) - 0.5f;
        float noiseY = Mathf.PerlinNoise(0f, Time.time * distortionSpeed) - 0.5f;
        Vector2 offset = new Vector2(noiseX, noiseY) * maxShake * t;
        rect.anchoredPosition = originalPos + offset;

        //float scale = 1f + (t * 0.05f);
       // rect.localScale = new Vector3(scale, scale, 1f);

        //float rot = Mathf.Sin(Time.time * distortionSpeed) * t * 2f;
       // rect.rotation = Quaternion.Euler(0, 0, rot);

        // Scale distortion
        float scale = 1f + (t * maxScaleEffect);
        rect.localScale = originalScale * scale;

        // Rotation distortion
        float rot = Mathf.Sin(Time.time * distortionSpeed * 2f) * t * maxRotationEffect;
        rect.localRotation = originalRotation * Quaternion.Euler(0, 0, rot);

    }
    public void ResetUI()
    {
        if (rect != null)
        {
            rect.anchoredPosition = originalPos;
            rect.localScale = originalScale;
            rect.localRotation = originalRotation;
        }
    }

}


