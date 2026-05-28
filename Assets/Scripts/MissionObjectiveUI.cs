using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MissionObjectiveUI : MonoBehaviour
{
    public GameObject objectivePanel;
    public TextMeshProUGUI objectiveText;
    public float displayDuration = 3f;
    public float fadeDuration = 1f;

    private Coroutine displayCoroutine;

    void Start()
    {
        if (objectivePanel != null) objectivePanel.SetActive(false);
    }

    public void ShowObjective(string objective)
    {
        if (objectivePanel == null || objectiveText == null) return;

        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        displayCoroutine = StartCoroutine(DisplayAndFade(objective));
    }

    private IEnumerator DisplayAndFade(string objective)
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
