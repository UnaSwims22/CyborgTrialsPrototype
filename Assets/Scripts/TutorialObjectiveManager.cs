using UnityEngine;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

public class TutorialObjectiveManager : MonoBehaviour
{
    public static TutorialObjectiveManager Instance;

    [System.Serializable]
    public class Objective
    {
        [Header("Objective")]
        [TextArea(2,4)]
        public string objectiveText;

        //[TextArea]
        //public string subtitleText;

        [Header("Mission Director Dialogue")]
        public DialogueLine dialogue;

        //public AudioClip narrationClip;
    }

   // [SerializeField] private DialogueLine introDialogue;

    [Header("References")]
    [SerializeField] private MissionObjectiveUI missionObjectiveUI;
    [SerializeField] private MissionDirectorNarration narrationSystem;

    [Header("Objectives")]
    [SerializeField] public List<Objective> objectives = new List<Objective>();
    
    private int currentObjectiveIndex = -1;

    public int CurrentObjectiveIndex => currentObjectiveIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

       // Instance = this;
    }

  //  void Start()
   // {
      //  StartTutorial();
    //}

    public void StartTutorial()
    {
        currentObjectiveIndex = -1;
        AdvanceObjective();
    }

    public void AdvanceObjective()
    {
        currentObjectiveIndex++;

        if (currentObjectiveIndex >= objectives.Count)
        {
            TutorialCompleted();
            Debug.Log("Tutorial Complete!");
            return;
        }

        Objective currentObjective = 
            objectives[currentObjectiveIndex];

        // Show UI objective
        if (missionObjectiveUI != null)
        {
            missionObjectiveUI.ShowObjective(
                currentObjective.objectiveText
            );
        }

        // Play narration
        if (narrationSystem != null &&
            currentObjective.dialogue != null)
        {
            narrationSystem.PlayDialogue(
                currentObjective.dialogue
                //current.narrationClip,
                //current.subtitleText
            );
        }
        else
        {
            Debug.Log(
                $"Tutorial Objective {currentObjectiveIndex}: {currentObjective.objectiveText}"
                );

        }

             //}\" New Objective: " + current.objectiveText);

        //AdvancedObjective();
    }

    public bool IsCurrentObjective(int index)
    {
        return currentObjectiveIndex == index;
    }

    private void TutorialCompleted()
    {
        Debug.Log("Tutorial Complete!");

        if (missionObjectiveUI != null)
        {
            missionObjectiveUI.ShowObjective(
                "Training Complete - Enter the Portal"
            );
        }
    }

    //public int CurrentObjectiveIndex()
    //{
      //  return currentObjectiveIndex;
    //}
}
