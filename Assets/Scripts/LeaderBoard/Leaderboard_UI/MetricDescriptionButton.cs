using UnityEngine;
using System.Collections;

public class MetricDescriptionButton : MonoBehaviour
{
    public GameObject descriptionPanel;

    private Coroutine animationRoutine;


    public void ToggleDescription()
    {
        MetricDescriptionManager.Instance
            .TogglePanel(descriptionPanel);

       // MetricDescriptionManager.Instance
         //   .TogglePanel(descriptionPanel);

        if (descriptionPanel.activeSelf)
        {
            if (animationRoutine != null)
            {
                StopCoroutine(animationRoutine);
            }

            animationRoutine =
                StartCoroutine(AnimatePanel());
        }
    }

    IEnumerator AnimatePanel()
    {
        RectTransform rect =
            descriptionPanel.GetComponent<RectTransform>();

        rect.localScale = Vector3.zero;

        float timer = 0f;

        while (timer < 0.15f)
        {
            timer += Time.deltaTime;

            rect.localScale =
                Vector3.Lerp(
                    Vector3.zero,
                    Vector3.one,
                    timer / 0.15f
                );

            yield return null;
        }

        rect.localScale = Vector3.one;
    }
}
