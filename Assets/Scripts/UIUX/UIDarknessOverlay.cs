using UnityEngine;
using UnityEngine.UI;

public class UIDarknessOverlay : MonoBehaviour
{
  
    public Image darknessOverlay;
    public float maxAlpha = 0.7f; // how dark UI gets

    void Update()
    {
        if (DarknessSystem.Instance == null) return;

        float darkness = DarknessSystem.Instance.darknessLevel;
        float maxDarkness = DarknessSystem.Instance.maxDarkness;

        float t = darkness / maxDarkness;

        Color color = darknessOverlay.color;
        color.a = Mathf.Lerp(0f, maxAlpha, t);

        darknessOverlay.color = color;
    }
}

