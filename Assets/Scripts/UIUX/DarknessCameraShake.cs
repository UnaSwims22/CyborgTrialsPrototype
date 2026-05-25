using UnityEngine;

public class DarknessCameraShake : MonoBehaviour
{
    //public Transform cameraHolder;
    public float shakeStrength = 0.2f;
    public float shakeSpeed = 15f;

    public Vector3 originalLocalPos;

    void Start()
    {
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (UIExposureController.Instance == null)
            return;


        float t = UIExposureController.Instance.HighIntensity;

        if (t <= 0f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalLocalPos,
                Time.deltaTime * 5f
            );
            return;
        }

        float noisex = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f);
        float noisey = (Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f);

        //cameraHolder.localPosition = new Vector3(x, y, 0) * shakeStrength * t;
        Vector3 shakeOffset = new Vector3(noisex, noisey, 0f) * shakeStrength * t;
        transform.localPosition = originalLocalPos + shakeOffset;

    }
}
