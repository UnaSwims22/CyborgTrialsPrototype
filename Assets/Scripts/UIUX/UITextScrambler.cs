using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UITextScrambler : MonoBehaviour
{
    public TMP_Text targetText;

    [TextArea]
    public string originalText;

    public string corruptionChars = "@#$%&*!?/<>[]{}";

    public float scrambleSpeed = 0.05f;

    private float timer;

    void Start()
    {
        originalText = targetText.text;
    }

    void Update()
    {
        float t = UIExposureController.Instance.HighIntensity;

        if (t <= 0f)
        {
            targetText.text = originalText;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= scrambleSpeed)
        {
            timer = 0f;

            StringBuilder sb = new StringBuilder();

            foreach (char c in originalText)
            {
                if (Random.value < t * 0.5f)
                {
                    sb.Append(corruptionChars[Random.Range(0, corruptionChars.Length)]);
                }
                else
                {
                    sb.Append(c);
                }
            }

            targetText.text = sb.ToString();
        }
    }
}
