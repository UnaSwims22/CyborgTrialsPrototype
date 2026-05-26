using UnityEngine;

public class HologramFlicker : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float flickerSpeed = 0.05f;
    public float minAlpha = 0.75f;
    public float maxAlpha = 1f;

    void Update()
    {
        canvasGroup.alpha =
            Random.Range(minAlpha, maxAlpha);
    }
}
