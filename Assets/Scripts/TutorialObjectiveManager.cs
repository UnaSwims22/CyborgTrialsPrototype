using UnityEngine;
using System.Collections.Generic;

public class TutorialObjectiveManager : MonoBehaviour
{
    public static TutorialObjectiveManager Instance;

    [System.Serializable]
    public class Objective
    {
        [TextArea]
        public string objectiveText;

        [TextArea]
        public string subtitleText;

        public AudioClip narrationClip;
    }

    [Header("References")]
    public MissionObjectiveUI missionObjectiveUI;
    public MissionDirectorNarration narrationSystem;

    [Header("Objectives")]
    public List<Objective> objectives = new List<Objective>();

    private int currentObjectiveIndex = -1;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartTutorial();
    }

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
            Debug.Log("Tutorial Complete!");
            return;
        }

        Objective current = objectives[currentObjectiveIndex];

        // Show UI objective
        if (missionObjectiveUI != null)
        {
            missionObjectiveUI.ShowObjective(current.objectiveText);
        }

        // Play narration
        if (narrationSystem != null)
        {
            narrationSystem.PlayNarration(
                current.narrationClip,
                current.subtitleText
            );
        }

        Debug.Log("New Objective: " + current.objectiveText);
    }

    public int CurrentObjectiveIndex()
    {
        return currentObjectiveIndex;
    }
}
