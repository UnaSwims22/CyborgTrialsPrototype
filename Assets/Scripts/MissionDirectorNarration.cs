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
    }


    void Start()
    {
        // if (subtitleText != null) subtitleText.gameObject.SetActive(false);

        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);
        //subtitlePanel.SetActive(true);
    }

    public void PlayDialogue(DialogueLine line)
    {
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
        subtitlePanel.SetActive(true);

        speakerText.text = line.speakerName;
        subtitleText.text = line.subtitleText;

        narrationAudioSource.clip = line.audioClip;
        narrationAudioSource.Play();

        yield return new WaitForSeconds(
            line.audioClip.length);

        subtitlePanel.SetActive(false);
    }

   // private IEnumerator DisplaySubtitle(string subtitle, float audioLength)
   // {
     //   subtitleText.text = subtitle;
     //   subtitleText.gameObject.SetActive(true);

      //  yield return new WaitForSeconds(audioLength);

       // subtitleText.gameObject.SetActive(false);
   // }

   // public void StopNarration()
   // {
   //     if (narrationAudioSource != null) narrationAudioSource.Stop();
    //    if (subtitleCoroutine != null)
     //   {
      //      StopCoroutine(subtitleCoroutine);
      //      subtitleText.gameObject.SetActive(false);
      //  }
   // }
}
