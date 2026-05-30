using UnityEngine;
using TMPro;
using System.Collections;

public class MissionDirector : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Subtitles")]
    [SerializeField] private TextMeshProUGUI subtitleText;

    [Header("Dialogue")]
    [SerializeField] private DialogueLine[] lines;

    private int currentLine = 0;

    public void PlayLine(int index)
    {
        StartCoroutine(PlayDialogue(index));
    }

    IEnumerator PlayDialogue(int index)
    {
        DialogueLine line = lines[index];

        subtitleText.text = line.subtitleText;
        subtitleText.gameObject.SetActive(true);

        audioSource.clip = line.audioClip;
        audioSource.Play();

        yield return new WaitForSeconds(line.subtitleDuration);

        subtitleText.gameObject.SetActive(false);
    }
}
