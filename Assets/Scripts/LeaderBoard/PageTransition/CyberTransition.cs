using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CyberTransition : MonoBehaviour
{
    public CanvasGroup overlay;

    public RectTransform scanLine;

    public float transitionTime = 1.5f;

    IEnumerator Start()
    {
        overlay.alpha = 1f;

        yield return StartCoroutine(
            ScanReveal()
        );

        yield return StartCoroutine(
            FadeOut()
        );
    }


    IEnumerator ScanReveal()
    {
        Vector2 start =
            new Vector2(0, 1200);

        Vector2 end =
            new Vector2(0, -1200);

        float timer = 0f;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;

            float t =
                Mathf.SmoothStep(
                    0,
                    1,
                    timer / transitionTime
                );

            scanLine.anchoredPosition =
                Vector2.Lerp(
                    start,
                    end,
                    t
                );

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;

            overlay.alpha =
                Mathf.Lerp(
                    1,
                    0,
                    timer
                );

            yield return null;
        }

        overlay.gameObject.SetActive(false);
    }
}
