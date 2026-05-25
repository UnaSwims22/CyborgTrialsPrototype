using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    public CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;

    private float defaultAmplitude;
    private float defaultFrequency;

    private void Awake()
    {
        Instance = this;
        Debug.Log("CameraShake Awake on: " + gameObject.name);
        //vcam = GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        defaultAmplitude = noise.m_AmplitudeGain;
        defaultFrequency = noise.m_FrequencyGain;
    }

    public void Shake(float intensity, float frequency, float time)
    {
        Debug.Log("CameraShake running on: " + gameObject.name);

        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("CameraShake object is INACTIVE!");
            return;
        }

        
        StartCoroutine(ShakeRoutine(intensity, frequency, time));
    }

    IEnumerator ShakeRoutine(float intensity, float frequency, float time)
    {
        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = frequency;

        yield return new WaitForSeconds(time);

        noise.m_AmplitudeGain = defaultAmplitude;
        noise.m_FrequencyGain = defaultFrequency;

       // float timer = 0;

        //while (timer < time)
       // {
            //timer += Time.deltaTime;
            //yield return null;
       // }

        //noise.m_AmplitudeGain = 0;
    }
}

   
