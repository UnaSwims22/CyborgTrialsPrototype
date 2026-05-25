using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Concurrent;


public class TransitionController : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI glitchText;

    [Header("Screen Pulse")]
    public CanvasGroup pulsePanel;

    [Header("Camera")]
    public CameraScript cameraShake;
    public Camera mainCamera;

    public float countdownTime = 10f;
    public string nextSceneName = "SampleScene";

    

    //[Header("Audio Input")]
    //public AudioSource audioSource;
    //public AudioClip beep;
    //public AudioClip teleport;

    //public CanvasGroup canvasGroup;
    // public Image fadeImage;

    [Header("Glitch Settings")]
    public float glitchStartTime = 5f;
    public float glitchInterval = 0.08f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip normalLoop;
    public AudioClip glitchLoop;

    //[Header("Camera Shake")]
    //public Transform cameraTransform;
    //public float shakeIntensity = 1f;
    //public float shakeSpeed = 20f;

    private bool isGlitching = false;
    //private Vector3 originalCamPos;

     void Start()
    {
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1.5f);

        // Play normal sound
        audioSource.clip = normalLoop;
        audioSource.loop = true;
        audioSource.Play();


        StartCoroutine(TransitionRoutine());

        //audioSource.PlayOneShot(beep);
        //yield return new WaitForSeconds(1f);

       // cameraShake.StartShake();

    }

    void StartGlitchEffects()
    {
        // AUDIO SWITCH
        audioSource.Stop();
        audioSource.clip = glitchLoop;
        audioSource.loop = true;
        audioSource.Play();

        loadingText.text = "";

        StartCoroutine(GlitchEffect());
        StartCoroutine(ScreenPulse());
        StartCoroutine(FOVGlitch());

        if (cameraShake != null)
            cameraShake.StartShake();
    }

    
    IEnumerator TransitionRoutine()
    {
        //yield return StartCoroutine(Fade(0, 1, 1f));

        loadingText.text = "INITIALIZING...";
        yield return new WaitForSeconds(1f);

        loadingText.text = "LOCKING COORDINATES...";
        yield return new WaitForSeconds(1f);

        float timer = countdownTime;

        while (timer > 0)
        {
            countdownText.text = Mathf.Ceil(timer).ToString();

            // Trigger glitch at halfway
            if (!isGlitching && timer <= glitchStartTime)
            {
                StartGlitchEffects();
               // StartCoroutine(GlitchEffect());
                //StartCoroutine(CameraShake());
               // StartCoroutine(FOVGlitch());
                isGlitching = true;
            }

            // Pulse effect
            // float scale = 1f + Mathf.Sin(Time.time * 5f) * 0.1f;
            //countdownText.transform.localScale = new Vector3(scale, scale, scale);

            timer -= Time.deltaTime;
            yield return null;
        }

       

        loadingText.text = "TELEPORTING...";
        yield return new WaitForSeconds(0.5f);

       

        // Load next scene immediately
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator GlitchEffect()
    {
        string[] glitchMessages = {
            "ERROR",
            "RETRYING...",
            "SIGNAL LOST",
            "DATA CORRUPTED",
            "NULL",
            "???",
           // "..."
        };

       // if (isGlitching)
        //{
         //   countdownText.text = UnityEngine.Random.Range(0, 9).ToString();
        //}

       // if (timer <= glitchStartTime && !isGlitching)
        //{
           // isGlitching = true;

            // SWITCH AUDIO IMMEDIATELY
           // audioSource.Stop();
            //audioSource.clip = glitchLoop;
           // audioSource.loop = true;
            //audioSource.Play();

           // StartCoroutine(GlitchEffect());
           // StartCoroutine(ScreenPulse());

           // if (cameraShake != null)
             //   cameraShake.StartShake();
       // }

        while (true)
        {
            Time.timeScale = UnityEngine.Random.Range(0.7f, 1.3f);

            countdownText.text = Random.Range(0, 9).ToString();
            glitchText.text = glitchMessages[UnityEngine.Random.Range(0, glitchMessages.Length)];
            //loadingText.text = glitchMessages[Random.Range(0, glitchMessages.Length)];

            // Random jitter for text

            glitchText.transform.localPosition = new Vector3(
           UnityEngine.Random.Range(-20f, 20f),
           UnityEngine.Random.Range(-20f, 20f),
           0
       );
            //loadingText.transform.localPosition =
              //  new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

            yield return new WaitForSeconds(glitchInterval);
        }
    }

    IEnumerator ScreenPulse()
    {
        while (true)
        {
            pulsePanel.alpha = UnityEngine.Random.Range(0f, 0.6f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FOVGlitch()
    {
        float baseFOV = mainCamera.fieldOfView;

        while (true)
        {
            mainCamera.fieldOfView = baseFOV + UnityEngine.Random.Range(-20f, 20f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    
}
