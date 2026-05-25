using UnityEngine;

public class SirenTrigger : MonoBehaviour
{
    public AudioSource sirenAudio;

    public Light flickerLight;

    void Update()
    {
        if (sirenAudio.isPlaying && flickerLight != null)
        {
            sirenAudio.volume = flickerLight.intensity / 2f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!sirenAudio.isPlaying)
            {
                sirenAudio.Play();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = sirenAudio
            ? new Color(0f, 1f, 0f, 0.3f)
            : new Color(1f, 1f, 0f, 0.2f);

        Gizmos.matrix = transform.localToWorldMatrix;

        // Draw based on attached collider type
        if (TryGetComponent<BoxCollider>(out var box))
            Gizmos.DrawCube(box.center, box.size);
        else if (TryGetComponent<SphereCollider>(out var sphere))
            Gizmos.DrawSphere(sphere.center, sphere.radius);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sirenAudio.isPlaying)
            {
                //sirenAudio.Stop();
                sirenAudio.volume = Mathf.Lerp(sirenAudio.volume, 0f, Time.deltaTime * 5f);
            }
        }
    }
}

