using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UITextScrambler : MonoBehaviour
{
    public TMP_Text targetText;

   // [TextArea]
    //public string originalText;

    public string corruptionChars = "@#$%&*!?/<>[]{}";

    public float scrambleSpeed = 0.05f;

    private float timer;
    private string _originalText;

    void Start()
    {
        // Store the initial text when the script starts
        if (targetText != null)
        {
            _originalText = targetText.text;
        }

        //  originalText = targetText.text;
    }


    void Update()
    {
        if (targetText == null) return;
        if (UIExposureController.Instance == null) return;

        float t = UIExposureController.Instance.HighIntensity;

        
        
        //liveText = targetText.text;

        if (t <= 0f)
        {
            if (targetText.text != _originalText)
            {
                targetText.text = _originalText;
            }
            return;

            //targetText.text = originalText;
            //return;
        }

        timer += Time.deltaTime;

        if (timer >= scrambleSpeed)
        {
            timer = 0f;

            StringBuilder sb = new StringBuilder();

            foreach (char c in _originalText)
            {
                // preserve spaces
                if (c == ' ')
                {
                    sb.Append(' ');
                    continue;
                }

                // corrupt some characters
                if (Random.value < t * 0.5f)
                {
                    sb.Append(
                        corruptionChars[
                            Random.Range(0, corruptionChars.Length)
                        ]
                    );
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
