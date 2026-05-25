using UnityEngine;

public class UIGlitch : MonoBehaviour
{
    RectTransform rect;

    Vector2 original;

    public float intensity = 4f;

    void Start()
    {
        rect =
            GetComponent<RectTransform>();

        original =
            rect.anchoredPosition;
    }

    void Update()
    {
        if (Random.value > 0.96f)
        {
            rect.anchoredPosition =
                original +
                Random.insideUnitCircle *
                intensity;
        }
        else
        {
            rect.anchoredPosition =
                original;
        }
    }
}
