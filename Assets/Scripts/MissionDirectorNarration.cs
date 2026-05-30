using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Audio;

public class MissionDirectorNarration : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource narrationAudioSource;

    [Header("Subtitles")]
    public GameObject subtitlePanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI subtitleText;

    // public float subtitleDisplayDuration = 3f;

    private Coroutine currentRoutine;
    //private Coroutine subtitleCoroutine;
    //private Coroutine subtitleRoutine;

    void Awake()
    {
        // Ensure system is always active
        gameObject.SetActive(true);
        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);
    }


    void Start()
    {
        // if (subtitleText != null) subtitleText.gameObject.SetActive(false);

        //if (subtitlePanel != null)
           // subtitlePanel.SetActive(false);
        //subtitlePanel.SetActive(true);
    }

    public void PlayDialogue(DialogueLine line)
    {


        Debug.Log("PLAYING: " + line.subtitleText);

        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("MissionDirector is inactive!");
            return;
        }

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(PlayDialogueRoutine(line));

       // if (currentRoutine != null)
       // {
       //     StopCoroutine(currentRoutine);
       // }

       // currentRoutine = StartCoroutine(
        //   PlayDialogueRoutine(line));
    }

  

    IEnumerator PlayDialogueRoutine(DialogueLine line)
    {
        if (line == null)
        {
            Debug.LogWarning("DialogueLine is NULL");
            yield break;
        }


        Debug.Log("Subtitle panel active BEFORE: " + subtitlePanel.activeSelf);
        subtitlePanel.SetActive(true);
        Debug.Log("Subtitle panel active AFTER: " + subtitlePanel.activeSelf);

        Debug.Log("Speaker = " + line.speakerName);
        Debug.Log("Subtitle = " + line.subtitleText);

        speakerText.text = line.speakerName;
        subtitleText.text = line.subtitleText;

        if (line.audioClip != null)
        {
            narrationAudioSource.clip = line.audioClip;
            narrationAudioSource.Play();

            yield return new WaitForSeconds(line.audioClip.length);
        }
        else
        {
            Debug.LogWarning(
                $"No audio clip assigned for dialogue: {line.subtitleText}"
            );

            yield return new WaitForSeconds(3f);
        }

        subtitlePanel.SetActive(false);
    }

}

   

