using UnityEngine;
using TMPro;

public class WorldSpaceHUD : MonoBehaviour
{
    public Transform playerCamera;
    public Vector3 offset;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI batteryText;

    void LateUpdate()
    {
        if (playerCamera == null) return;

        // Position the HUD relative to the player camera
        transform.position = playerCamera.position + playerCamera.TransformDirection(offset);

        // Make the HUD always face the camera
        transform.LookAt(transform.position + playerCamera.rotation * Vector3.forward, playerCamera.rotation * Vector3.up);
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        }
    }

    public void UpdateBattery(float currentBattery, float maxBattery)
    {
        if (batteryText != null)
        {
            batteryText.text = "Battery: " + Mathf.RoundToInt(currentBattery) + "% ";
        }
    }
}
