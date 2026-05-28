using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ObjectiveSystem : MonoBehaviour
{
    public static ObjectiveSystem Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject objectivePanel; // The UI panel that holds the objective text.
    [SerializeField] private TMP_Text objectiveText; // The TextMeshPro component to display the current objective.

    [Header("Objective Settings")]
    [SerializeField] private float displayDuration = 3f; // How long an objective is displayed if not completed immediately.
    [SerializeField] private float fadeDuration = 0.5f; // How long it takes for the objective text to fade in/out.

    private Queue<Objective> objectiveQueue = new Queue<Objective>(); // A queue to hold upcoming objectives.
    private Objective currentObjective; // The objective currently being displayed.
    private Coroutine displayCoroutine; // Reference to the current display coroutine to stop it if needed.

    // Represents a single objective with its description and a condition for completion.
    public class Objective
    {
        public string description; // The text description of the objective.
        public System.Func<bool> completionCondition; // A function that returns true when the objective is met.

        public Objective(string desc, System.Func<bool> condition)
        {
            description = desc;
            completionCondition = condition;
        }
    }

    
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        objectivePanel.SetActive(false); // Ensure the objective panel is hidden at start.
    }

    // Update is called once per frame.
    private void Update()
    {
        // If there's a current objective and its completion condition is met, complete it.
        if (currentObjective != null && currentObjective.completionCondition != null && currentObjective.completionCondition())
        {
            CompleteCurrentObjective();
        }
    }

    // Adds a new objective to the queue.
    public void AddObjective(string description, System.Func<bool> completionCondition = null)
    {
        objectiveQueue.Enqueue(new Objective(description, completionCondition));
        // If no objective is currently being displayed, start displaying the next one.
        if (currentObjective == null && displayCoroutine == null)
        {
            StartNextObjective();
        }
    }

    // Starts displaying the next objective in the queue.
    private void StartNextObjective()
    {
        if (objectiveQueue.Count > 0)
        {
            currentObjective = objectiveQueue.Dequeue(); // Get the next objective.
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }
            displayCoroutine = StartCoroutine(DisplayObjectiveCoroutine(currentObjective.description));
        }
        else
        {
            currentObjective = null; // No more objectives.
        }
    }

    // Coroutine to display an objective with fade-in and fade-out effects.
    private IEnumerator DisplayObjectiveCoroutine(string description)
    {
        objectiveText.text = description; // Set the objective text.
        objectivePanel.SetActive(true); // Make the panel visible.

        // Fade in
        CanvasGroup canvasGroup = objectivePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = objectivePanel.AddComponent<CanvasGroup>(); // Add CanvasGroup if not present.
        }
        canvasGroup.alpha = 0f;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Wait for completion or duration
        float objectiveTimer = 0f;
        while (currentObjective != null && !currentObjective.completionCondition() && objectiveTimer < displayDuration)
        {
            objectiveTimer += Time.deltaTime;
            yield return null;
        }

        // If the objective was completed by condition, it will be handled by CompleteCurrentObjective.
        // If it timed out, or there's no condition, just fade out.
        if (currentObjective != null && !currentObjective.completionCondition())
        {
            CompleteCurrentObjective(); // Force complete if timed out.
        }
    }

    // Completes the current objective and starts the next one.
    public void CompleteCurrentObjective()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        displayCoroutine = StartCoroutine(FadeOutObjectiveCoroutine());
        currentObjective = null; // Clear the current objective.
    }

    // Coroutine to fade out the objective panel.
    private IEnumerator FadeOutObjectiveCoroutine()
    {
        CanvasGroup canvasGroup = objectivePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = objectivePanel.AddComponent<CanvasGroup>();
        }
        float timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        objectivePanel.SetActive(false); // Hide the panel after fading out.
        displayCoroutine = null; // Clear the coroutine reference.
        StartNextObjective(); // Try to start the next objective.
    }

    // Call this method to manually complete an objective from another script.
    public void ManuallyCompleteObjective(string description)
    {
        // This method can be used if an objective needs to be completed by an external event
        // that isn't easily captured by a continuous completionCondition check.
        // For example, if an animation finishes or a specific interaction occurs.
        if (currentObjective != null && currentObjective.description == description)
        {
            CompleteCurrentObjective();
        }
        else
        {
            // Optionally, remove from queue if it's an upcoming objective.
            // This would require iterating through the queue and recreating it, or using a List instead of Queue.
            Debug.LogWarning($"Attempted to manually complete objective '{description}' but it was not the current objective.");
        }
    }
}
