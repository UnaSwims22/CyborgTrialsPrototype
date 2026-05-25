using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public IEnumerator CountTo(int target)
    {
        float duration = 2f;

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

            int value =
                Mathf.RoundToInt(
                    Mathf.Lerp(
                        0,
                        target,
                        t
                    )
                );

            scoreText.text =
                value.ToString();

            yield return null;
        }

        scoreText.text =
            target.ToString();
    }
}
