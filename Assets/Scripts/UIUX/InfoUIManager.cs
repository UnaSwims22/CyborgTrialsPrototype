using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
public class InfoUIManager : MonoBehaviour
{
    public static InfoUIManager Instance { get; private set; }


    [Header("Red Clues")]
    [SerializeField] private TextMeshProUGUI redClueText;
    [SerializeField] private int totalRedClues = 4;
    private int currentRedClues = 0;

    [Header("Blue Clues")]
    [SerializeField] private TextMeshProUGUI blueClueText;
    [SerializeField] private int totalBlueClues = 2;
    private int currentBlueClues = 0;

    [Header("Green Clues")]
    [SerializeField] private TextMeshProUGUI greenClueText;
    [SerializeField] private int totalGreenClues = 2;
    private int currentGreenClues = 0;

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

    [Header("Clue System")]
    [SerializeField] private TextMeshProUGUI clueCounterText;

    private int currentClues = 0;
    [SerializeField] private int totalClues = 6;

    private Coroutine fadeCoroutine;



  


    private void Awake()
    {
        Debug.Log("Red UI object: " + redClueText.name);

        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

       // DontDestroyOnLoad(gameObject);

        // Make sure panel is hidden at start
        infoPanel.SetActive(false);
        canvasGroup.alpha = 0f;

        UpdateClueUI();
        UpdateRedUI();
        UpdateBlueUI();
        UpdateGreenUI();

    }

    public void AddRedClue()
    {
        currentRedClues++;
        Debug.Log("RED clue count is now: " + currentRedClues);

        currentRedClues = Mathf.Clamp(currentRedClues, 0, totalRedClues);

       // ClueData newClue = new ClueData
        //{
        //    title = title,
         //   description = description
       // };

       // collectedClues.Add(newClue);

        UpdateRedUI();



       // if (audioSource && redClueSound)
           // audioSource.PlayOneShot(redClueSound);
    }

    


    public void AddBlueClue()
    {
        
        currentBlueClues++;

        Debug.Log("BLUE clue count is now: " + currentBlueClues);

        currentBlueClues = Mathf.Clamp(currentBlueClues, 0, totalBlueClues);

        //ClueData newClue = new ClueData
       // {
        //    title = title,
       //     description = description
       // };

        //collectedClues.Add(newClue);

        UpdateBlueUI();

       // if (audioSource && blueClueSound)
            //audioSource.PlayOneShot(blueClueSound);
    }

    public void AddGreenClue()
    {

        currentGreenClues++;

        Debug.Log("BLUE clue count is now: " + currentGreenClues);

        currentGreenClues = Mathf.Clamp(currentGreenClues, 0, totalGreenClues);

        //ClueData newClue = new ClueData
        // {
        //    title = title,
        //     description = description
        // };

        //collectedClues.Add(newClue);

        UpdateGreenUI();

        // if (audioSource && blueClueSound)
        //audioSource.PlayOneShot(blueClueSound);
    }

    private void UpdateRedUI()
    {
       redClueText.text = currentRedClues + "/" + totalRedClues + " Red Clues";
        //redClueText.text = "Red Clues Collected: " + currentRedClues + "/" + totalRedClues;
        Debug.Log("Updating RED UI to: " + currentRedClues);
    }

    private void UpdateBlueUI()
    {
        //blueClueText.text = "Blue Clues Collected: " + currentBlueClues + "/" + totalBlueClues; //+ " Blue Clues";
        blueClueText.text = currentBlueClues + "/" + totalBlueClues + " Blue Clues";
    }

    private void UpdateGreenUI()
    {
        greenClueText.text = currentGreenClues + "/" + totalGreenClues + " Green Clues";
    }

    public void AddClue()
    {
        currentClues++;
        currentClues = Mathf.Clamp(currentClues, 0, totalClues);

        UpdateClueUI();
    }

    private void UpdateClueUI()
    {
        clueCounterText.text = currentClues + "/" + totalClues + " Total Clues";
        //clueCounterText.text = "Total Clues Collected: " + currentClues + "/" + totalClues;
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
