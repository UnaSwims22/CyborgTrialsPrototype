using UnityEngine;
using System.Collections;

public class TutorialNarrationSequence : MonoBehaviour
{
    public MissionDirectorNarration narrator;

    public DialogueLine[] introductionLines;

    private IEnumerator Start()
    {
        foreach (DialogueLine line in introductionLines)
        {
            narrator.PlayDialogue(line);

            yield return new WaitForSeconds(
                line.voiceClip.length + 0.5f);
        }
    }
}
