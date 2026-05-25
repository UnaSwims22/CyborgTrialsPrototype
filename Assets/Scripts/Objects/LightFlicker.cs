using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light flickerLight;

    [Header("Flicker Settings")]
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.5f;

    public float flickerSpeed = 0.05f;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
            timer = flickerSpeed;
        }
    }
}

