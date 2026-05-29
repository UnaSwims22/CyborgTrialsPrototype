using UnityEngine;

public class TutorialObjectiveTrigger : MonoBehaviour
{
    [Header("Settings")]
    public int requiredObjectiveIndex;

    public bool destroyAfterTrigger = true;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (!other.CompareTag("Player")) return;

        if (TutorialObjectiveManager.Instance.CurrentObjectiveIndex() == requiredObjectiveIndex)
        {
            triggered = true;

            Debug.Log("Objective Completed!");

            TutorialObjectiveManager.Instance.AdvanceObjective();

            if (destroyAfterTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}
