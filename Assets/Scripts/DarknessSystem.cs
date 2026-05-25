using UnityEngine;
using System;
using System.Collections;

public class DarknessSystem : MonoBehaviour
{

    public static DarknessSystem Instance { get; private set; }

    //--------------------------------------
    //New script

    public event Action<float> OnDarknessLevelChanged;
    public event Action OnEnterCompleteDarkness;
    public event Action OnExitCompleteDarkness;
    //--------------------------------------


    [Header("Darkness Settings")]
    [Range(0f, 100f)]
    public float darknessLevel = 0f; // 0 = safe, 100 = danger
    public float maxDarkness = 100f;
    public float completeDarknessThreshold = 95f;

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

    private bool inCompleteDarkness = false;

    void Awake()
    {
        //--------------------------

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        //--------------------------


        //Instance = this;
    }

    void Update()
    {
        UpdateDarkness();
        UpdateLighting();
        CheckCompleteDarkness();
    }

    void UpdateDarkness()
    {
        if (LightOrb.Instance != null && LightOrb.Instance.IsLightOn)
        {
            darknessLevel -= decreaseRate * Time.deltaTime;
        }
        else
        {
            darknessLevel += increaseRate * Time.deltaTime;
        }

        darknessLevel = Mathf.Clamp(darknessLevel, 0, maxDarkness);
        OnDarknessLevelChanged?.Invoke(darknessLevel);

       // if (LightOrb.Instance == null) return;

        //if (LightOrb.Instance.IsLightOn)
        //{
       //     darknessLevel -= decreaseRate * Time.deltaTime;
        //}
        //else
       // {
          //  darknessLevel += increaseRate * Time.deltaTime;
        //}

       // darknessLevel = Mathf.Clamp(darknessLevel, 0, maxDarkness);
    }



    void UpdateLighting()
    {
        float t = darknessLevel / maxDarkness;
        
        if (directionalLight != null)
        {
            //float t = darknessLevel / maxDarkness;
            directionalLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, t);
        }

       if (useFog)
       {
           //float t = darknessLevel / maxDarkness;

           RenderSettings.fog = true;
            RenderSettings.fogDensity = Mathf.Lerp(minFogDensity, maxFogDensity, t);
       }
    }

    void CheckCompleteDarkness()
    {
        if (darknessLevel >= completeDarknessThreshold && !inCompleteDarkness)
        {
            inCompleteDarkness = true;
            OnEnterCompleteDarkness?.Invoke();
            Debug.Log("Entered complete darkness!");
        }
        else if (darknessLevel < completeDarknessThreshold && inCompleteDarkness)
        {
            inCompleteDarkness = false;
            OnExitCompleteDarkness?.Invoke();
            Debug.Log("Exited complete darkness.");
        }
    }

    public float GetNormalizedDarknessLevel()
    {
        return darknessLevel / maxDarkness;
    }

}

