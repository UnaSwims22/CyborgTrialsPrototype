using UnityEngine;
using UnityEngine.UI;

public class DarknessUI : MonoBehaviour
{
 
    public Slider lightSlider;

    void Update()
    {
        if (DarknessSystem.Instance == null) return;

        float darkness = DarknessSystem.Instance.darknessLevel;
        float maxDarkness = DarknessSystem.Instance.maxDarkness;

        // Invert: 0 darkness = full bar, 100 darkness = empty bar
        float normalizedLight = 1f - (darkness / maxDarkness);

        lightSlider.value = normalizedLight;
    }
}

