using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    
    [Header("Settings")]
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (GameManager.Instance.currentLives >= GameManager.Instance.maxLives)
        {
            Debug.Log("Already full health");
            return;
        }

        // Safety check
        if (GameManager.Instance == null) return;

        // Add life BUT clamp to maxLives
        GameManager.Instance.currentLives = Mathf.Clamp(
            GameManager.Instance.currentLives + healAmount,
            0,
            GameManager.Instance.maxLives
        );

        // Update UI
        UIManager.Instance?.UpdateHearts(GameManager.Instance.currentLives);

        Debug.Log("Picked up health. Lives: " + GameManager.Instance.currentLives);

        // Destroy pickup
        Destroy(gameObject);
    }
}

