using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RingGraphUI : MonoBehaviour
{
    [Header("UI")]
    public Image fillImage;
    public TextMeshProUGUI percentText;

    [Header("Colours")]
    public Gradient ringGradient;

    [Header("Pulse")]
    public bool pulseAtEnd = true;
    public Transform ringTransform;

    [Header("Animation")]
    public float animationDuration = 1.5f;
    public AnimationCurve fillCurve =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    

    public IEnumerator AnimateGraph(float targetPercent)
    {
        float timer = 0f;

        float targetFill =
            targetPercent / 100f;

        ringTransform.localScale =
            Vector3.one * 0.7f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            float normalized =
                timer / animationDuration;

            float eased =
                fillCurve.Evaluate(normalized);

            float currentFill =
                Mathf.Lerp(0f, targetFill, eased);

            fillImage.fillAmount =
                currentFill;

            Color currentColor =
                ringGradient.Evaluate(currentFill);

            // SAFE UI COLOUR CHANGE
            fillImage.color =
                currentColor;

            int value =
                Mathf.RoundToInt(
                    currentFill * 100
                );

            percentText.text =
                value + "%";

            float scale =
                Mathf.Lerp(
                    0.7f,
                    1f,
                    eased
                );

            ringTransform.localScale =
                Vector3.one * scale;

            yield return null;
        }

        fillImage.fillAmount =
            targetFill;

        percentText.text =
            Mathf.RoundToInt(targetPercent) + "%";

        if (pulseAtEnd)
        {
            yield return StartCoroutine(
                Pulse()
            );
        }
    }

    IEnumerator Pulse()
    {
        Vector3 original =
            ringTransform.localScale;

        Vector3 target =
            original * 1.12f;

        float timer = 0f;

        while (timer < 0.18f)
        {
            timer += Time.deltaTime;

            float t =
                Mathf.SmoothStep(
                    0,
                    1,
                    timer / 0.18f
                );

            ringTransform.localScale =
                Vector3.Lerp(
                    original,
                    target,
                    t
                );

            yield return null;
        }

        timer = 0f;

        while (timer < 0.2f)
        {
            timer += Time.deltaTime;

            float t =
                Mathf.SmoothStep(
                    0,
                    1,
                    timer / 0.2f
                );

            ringTransform.localScale =
                Vector3.Lerp(
                    target,
                    original,
                    t
                );

            yield return null;
        }

        ringTransform.localScale =
            original;
    }
}




