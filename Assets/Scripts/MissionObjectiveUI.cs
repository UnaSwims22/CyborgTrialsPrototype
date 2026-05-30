using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MissionObjectiveUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject objectivePanel;
    public TextMeshProUGUI objectiveText;

    [Header("Animation")]
    public float displayDuration = 3f;
    public float fadeDuration = 1f;

    private Coroutine displayCoroutine;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = objectivePanel.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = objectivePanel.AddComponent<CanvasGroup>();
        }

       // if (objectivePanel != null) objectivePanel.SetActive(false);
    }

    private void Start()
    {
        //objectivePanel.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowObjective(string objective)
    {
        Debug.Log("SHOWING OBJECTIVE: " + objective);

        // if (objectivePanel == null || objectiveText == null) return;
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        displayCoroutine = StartCoroutine(DisplayObjective(objective));
    }

    private IEnumerator DisplayObjective(string objective)
    {
        objectiveText.text = objective;
        objectivePanel.SetActive(true);
       
        
        
        CanvasGroup canvasGroup = objectivePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = objectivePanel.AddComponent<CanvasGroup>();
        }

        // Fade in
        canvasGroup.alpha = 0f;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(displayDuration);

        // Fade out
        timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        objectivePanel.SetActive(false);
    }
}
