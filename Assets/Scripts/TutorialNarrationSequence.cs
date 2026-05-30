using UnityEngine;
using System.Collections;
using StarterAssets;
using UnityEngine.InputSystem;

public class TutorialNarrationSequence : MonoBehaviour
{
    public MissionDirectorNarration narrator;

    public DialogueLine[] introductionLines;

    public PlayerInput playerInput;

    // public ThirdPersonController playerController;

    private IEnumerator Start()
    {
        //yield return null;

        //playerController.CanMove = false;

        // Disable player controls
        playerInput.enabled = false;

        foreach (DialogueLine line in introductionLines)
        {
            narrator.PlayDialogue(line);

            float waitTime = 3f;

            if (line.audioClip != null)
            {
                waitTime = line.audioClip.length + 0.5f;
            }

            yield return new WaitForSeconds(waitTime);

           // yield return new WaitForSeconds(
                //line.audioClip.length + 0.5f);
        }

        // Start objectives AFTER intro narration
        
        Debug.Log("INTRO FINISHED");
        //Debug.Log("Starting tutorial...");
        // Re-enable controls
        playerInput.enabled = true;

        TutorialObjectiveManager.Instance.StartTutorial();
        //playerController.CanMove = true;


    }
}
