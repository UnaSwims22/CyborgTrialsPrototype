using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class InfoUIManager_Tut : MonoBehaviour
{
  
    public static InfoUIManager_Tut Instance { get; private set; }


  

    //[Header("Audio")]
    //[SerializeField] private AudioSource audioSource;
    // [SerializeField] private AudioClip redClueSound;
    //[SerializeField] private AudioClip blueClueSound;

    [Header("UI References")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 3f;

    

    private int currentClues = 0;
    [SerializeField] private int totalClues = 3;

    private Coroutine fadeCoroutine;






    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        // Make sure panel is hidden at start
        infoPanel.SetActive(false);
        canvasGroup.alpha = 0f;

        

    }





    public void ShowInfo(string title, string body)
    {
        titleText.text = title;
        bodyText.text = body;
        infoPanel.SetActive(true);

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(1f));
    }

    public void HideInfo()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAndDisable());
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(
                canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime
            );
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
    }

    private IEnumerator FadeAndDisable()
    {
        yield return FadeTo(0f);
        infoPanel.SetActive(false);
    }

}

// Start is called once before the first execution of Update after the MonoBehaviour is created

