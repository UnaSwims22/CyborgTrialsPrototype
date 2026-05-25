using UnityEngine;
using System.Collections;
public class UIPanelMorph : MonoBehaviour
{
    public CanvasGroup group;

    public float duration = 0.8f;

    Vector3 originalScale;

    void Awake()
    {
        originalScale =
            transform.localScale;

        transform.localScale =
            new Vector3(
                0.2f,
                1f,
                1f
            );

        group.alpha = 0f;
    }

    public IEnumerator Morph()
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t =
                Mathf.SmoothStep(
                    0,
                    1,
                    timer / duration
                );

            transform.localScale =
                Vector3.Lerp(
                    new Vector3(
                        0.2f,
                        1f,
                        1f
                    ),
                    originalScale,
                    t
                );

            group.alpha = t;

            yield return null;
        }
    }
}
