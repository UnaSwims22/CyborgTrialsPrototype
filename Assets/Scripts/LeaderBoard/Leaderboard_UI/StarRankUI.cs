using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StarRankUI : MonoBehaviour
{
    public List<Image> stars;

    public Color activeColor =
        new Color(1f, 0.7f, 0f);

    public Color inactiveColor =
        new Color(0.2f, 0.2f, 0.2f);

    public IEnumerator AnimateStars(int starCount)
    {
        // Reset
        foreach (Image star in stars)
        {
            star.color = inactiveColor;

            star.transform.localScale =
                Vector3.one;
        }

        yield return new WaitForSeconds(0.5f);

        // Reveal stars
        for (int i = 0; i < starCount; i++)
        {
            stars[i].color = activeColor;

            yield return StartCoroutine(
                PopStar(stars[i])
            );

            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator PopStar(Image star)
    {
        Vector3 original =
            Vector3.one;

        Vector3 enlarged =
            Vector3.one * 1.4f;

        float timer = 0f;

        while (timer < 0.1f)
        {
            timer += Time.deltaTime;

            star.transform.localScale =
                Vector3.Lerp(
                    original,
                    enlarged,
                    timer / 0.1f
                );

            yield return null;
        }

        timer = 0f;

        while (timer < 0.1f)
        {
            timer += Time.deltaTime;

            star.transform.localScale =
                Vector3.Lerp(
                    enlarged,
                    original,
                    timer / 0.1f
                );

            yield return null;
        }

        star.transform.localScale =
            original;
    }
}
