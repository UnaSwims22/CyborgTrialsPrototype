using UnityEngine;
using UnityEngine.UI;

public class ScanlineEffect : MonoBehaviour
{
    public RawImage scanlineImage;

    public float scrollSpeed = 200f;
    public float maxAlpha = 0.4f;

    private Rect uvRect;

    void Start()
    {
        uvRect = scanlineImage.uvRect;
    }

    void Update()
    {
        float t = UIExposureController.Instance.exposure;

        // increase visibility in darkness
        Color c = scanlineImage.color;
        c.a = Mathf.Lerp(0f, maxAlpha, t);
        scanlineImage.color = c;

        // scrolling lines
        uvRect.y += scrollSpeed * Time.deltaTime * t;
        scanlineImage.uvRect = uvRect;
    }
}
