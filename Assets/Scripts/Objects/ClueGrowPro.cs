using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class ClueGlowPro : MonoBehaviour
{
   
    [Header("References")]
    public Light clueLight;
    public Transform player;
    public AudioSource pulseAudio;

    [Header("Glow Settings")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 5f;

    [Header("Distance Settings")]
    public float maxDistance = 10f;

    [Header("Pulse Settings")]
    public float basePulseSpeed = 1f;
    public float maxPulseSpeed = 4f;

    private float pulseTimer;

    void Start()
    {
        if (clueLight == null)
            clueLight = GetComponentInChildren<Light>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (DarknessSystem.Instance == null || player == null) return;

        float darkness = DarknessSystem.Instance.darknessLevel / DarknessSystem.Instance.maxDarkness;

        //  Distance factor (closer = stronger)
        float distance = Vector3.Distance(transform.position, player.position);
        float proximity = 1f - Mathf.Clamp01(distance / maxDistance);

        //  Combine darkness + proximity
        float intensityFactor = Mathf.Clamp01(darkness + proximity);

        //  Pulse speed increases with danger
        float pulseSpeed = Mathf.Lerp(basePulseSpeed, maxPulseSpeed, intensityFactor);

        pulseTimer += Time.deltaTime * pulseSpeed;

        float pulse = (Mathf.Sin(pulseTimer) + 1f) / 2f;

        //  Final intensity
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, intensityFactor);
        intensity *= Mathf.Lerp(0.7f, 1.3f, pulse);

        clueLight.intensity = intensity;

        HandleAudioPulse(pulse, intensityFactor, proximity);
    }

    void HandleAudioPulse(float pulse, float intensityFactor, float proximity)
    {
        
            if (pulseAudio == null) return;

            // Only allow sound when player is close enough
            float soundThreshold = 0.3f; // tweak this (0.0–1.0)

            if (proximity < soundThreshold)
            {
                // Fade out if too far
                if (pulseAudio.isPlaying)
                {
                    //pulseAudio.Stop();
                    pulseAudio.volume = Mathf.Lerp(pulseAudio.volume, 0f, Time.deltaTime * 5f);

                }
                return;
            }

            // Trigger only at pulse peak (prevents looping spam)
            if (pulse > 0.98f && !pulseAudio.isPlaying)
            {
                pulseAudio.pitch = Mathf.Lerp(0.8f, 1.5f, intensityFactor);
                pulseAudio.volume = Mathf.Lerp(0.2f, 1f, proximity);

                pulseAudio.PlayOneShot(pulseAudio.clip);
            }
        }



       // if (pulseAudio == null) return;

        // Play sound at peak of pulse
       // if (pulse > 0.95f && !pulseAudio.isPlaying)
       // {
       //     pulseAudio.pitch = Mathf.Lerp(0.8f, 1.5f, intensityFactor);
        //    pulseAudio.volume = Mathf.Lerp(0.2f, 1f, intensityFactor);

         //   pulseAudio.Play();
        //}
    
}


