using UnityEngine;

public class MetricDescriptionManager : MonoBehaviour
{
    public static MetricDescriptionManager Instance;

    private GameObject currentOpenPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void TogglePanel(GameObject panel)
    {
        // SAME PANEL CLICKED
        if (currentOpenPanel == panel)
        {
            panel.SetActive(false);

            currentOpenPanel = null;

            return;
        }

        // CLOSE CURRENT
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
        }

        // OPEN NEW
        panel.SetActive(true);

        currentOpenPanel = panel;
    }
}
