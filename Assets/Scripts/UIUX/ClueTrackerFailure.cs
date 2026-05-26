using UnityEngine;
using UnityEngine.UI;

public class ClueTrackerFailure : MonoBehaviour
{
    public CanvasGroup group;
    public float blinkSpeed = 8f;

    private bool forcedOff;

    void Update()
    {
        float t = UIExposureController.Instance.exposure;

        // Early warning: blinking starts mid-darkness
        float blink = Mathf.Sin(Time.time * blinkSpeed);

        if (t > 0.4f)
        {
            group.alpha = Mathf.Lerp(1f, 0.3f, Mathf.Abs(blink));
        }

        // Late stage: UI completely cuts out
       // if (t > 0.75f)
        //{
        //    forcedOff = true;
        //}

        //if (forcedOff)
        //{
        //    group.alpha = Mathf.Lerp(group.alpha, 0f, Time.deltaTime * 2f);
        //}
    }
}
