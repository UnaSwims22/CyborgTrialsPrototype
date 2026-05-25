using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Settings")]
    public int powerAmount = 1;

    private LightOrb lightOrb;

    [System.Obsolete]
    private void Start()
    {
        // Find the LightOrb in the scene
        lightOrb = FindObjectOfType<LightOrb>();

        if (lightOrb == null)
        {
            Debug.LogError("LightOrb not found in scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        // Safety check
        if (lightOrb == null) return;

        // OPTIONAL: Prevent overfill (only if you want a max limit later)
        // if (lightOrb.GetPowerUps() >= maxPowerUps) return;

        // Add power-up (this already updates UI inside LightOrb)
        lightOrb.AddPowerUp(powerAmount);

        Debug.Log("Picked up power-up. Total bolts increased!");

        // Destroy pickup
        Destroy(gameObject);

        //GameSessionTracker.Instance.UsedBatteryPowerUp();
    }
}



