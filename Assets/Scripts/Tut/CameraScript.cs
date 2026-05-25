using UnityEngine;
using System.Collections;


public class CameraScript : MonoBehaviour
{
    public float intensity = 0.5f;
    public float rotationIntensity = 5f;

    private Vector3 originalPos;
    private Quaternion originalRot;

    void Awake()
    {
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        while (true)
        {
            transform.localPosition = originalPos +
                new Vector3(
                    Random.Range(-intensity, intensity),
                    Random.Range(-intensity, intensity),
                    0
                );

            transform.localRotation = Quaternion.Euler(
                Random.Range(-rotationIntensity, rotationIntensity),
                Random.Range(-rotationIntensity, rotationIntensity),
                Random.Range(-rotationIntensity, rotationIntensity)
            );

            yield return null;
        }
    }
}

