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
    private Coroutine subtitleCoroutine;
    private Coroutine subtitleRoutine;

    void Start()
    {
       // if (subtitleText != null) subtitleText.gameObject.SetActive(false);
        subtitlePanel.SetActive(false);
    }

    public void PlayDialogue(DialogueLine line)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(
            PlayDialogueRoutine(line));
    }

       // if (subtitleRoutine != null)
       // {
          //  StopCoroutine(subtitleRoutine);
       // }

       // subtitleRoutine = StartCoroutine(PlayNarrationRoutine(clip, subtitle));

       // if (narrationAudioSource == null) return;

       // narrationAudioSource.clip = clip;
       // narrationAudioSource.Play();

       // if (subtitleText != null && !string.IsNullOrEmpty(subtitle))
        //{
           // if (subtitleCoroutine != null)
           // {
           //     StopCoroutine(subtitleCoroutine);
           // }
           // subtitleCoroutine = StartCoroutine(DisplaySubtitle(subtitle, clip.length));
       // }
    //}

    //private IEnumerator PlayNarrationRoutine(AudioClip clip, string subtitle)
    //{
       // subtitlePanel.SetActive(true);

        //subtitleText.text = subtitle;

        //narrationAudioSource.clip = clip;
        //narrationAudioSource.Play();

        //yield return new WaitForSeconds(clip.length);
    
       // subtitlePanel.SetActive(false);
    //}

    IEnumerator PlayDialogueRoutine(DialogueLine line)
    {
        subtitlePanel.SetActive(true);

        speakerText.text = line.speakerName;
        subtitleText.text = line.subtitleText;

        narrationAudioSource.clip = line.voiceClip;
        narrationAudioSource.Play();

        yield return new WaitForSeconds(
            line.voiceClip.length);

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
