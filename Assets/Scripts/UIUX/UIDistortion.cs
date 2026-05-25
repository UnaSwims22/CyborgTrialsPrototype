using UnityEngine;

public class UIDistortion : MonoBehaviour
{
   
    public float maxShake = 30f;
    public float distortionSpeed = 5f;

    private RectTransform rect;
    private Vector2 originalPos;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
    }

    void Update()
    {
        
        
        if (DarknessSystem.Instance == null) return;

        float t = DarknessSystem.Instance.darknessLevel / DarknessSystem.Instance.maxDarkness;

        float noiseX = Mathf.PerlinNoise(Time.time * distortionSpeed, 0f) - 0.5f;
        float noiseY = Mathf.PerlinNoise(0f, Time.time * distortionSpeed) - 0.5f;

        Vector2 offset = new Vector2(noiseX, noiseY) * maxShake * t;

        float scale = 1f + (t * 0.05f);
        rect.localScale = new Vector3(scale, scale, 1f);

        float rot = Mathf.Sin(Time.time * distortionSpeed) * t * 2f;
        rect.rotation = Quaternion.Euler(0, 0, rot);

        rect.anchoredPosition = originalPos + offset;
    }
}

  
    //private RectTransform rect;
   // private Vector3 originalPos;

   // [Header("Distortion Settings")]
   //public float maxShake = 10f;
    //public float speed = 5f;

    //void Start()
    //{
    //    rect = GetComponent<RectTransform>();
    //    originalPos = rect.anchoredPosition;
    //}

   // void Update()
    //{
      //  if (DarknessSystem.Instance == null) return;

      //  float darknessPercent = DarknessSystem.Instance.darknessLevel / DarknessSystem.Instance.maxDarkness;

       // float shakeAmount = maxShake * darknessPercent;

       // Vector2 randomOffset = Random.insideUnitCircle * shakeAmount;

       // rect.anchoredPosition = Vector3.Lerp(
       //     rect.anchoredPosition,
       ////     originalPos + (Vector3)randomOffset,
        //    Time.deltaTime * speed
        //);
    //}


