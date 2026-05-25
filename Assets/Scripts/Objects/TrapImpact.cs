using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapImpact : MonoBehaviour
{
    public string playertag = "Player";

    public bool hasLanded = false;

    private bool hasKilledPlayer = false;

    [SerializeField] private ResetTrigger resetTrigger;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded)
        {
            hasLanded = true;

            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(7f, 5f, 0.4f);
            }
        }

        Debug.Log("Trap hit player");

        if (collision.gameObject.CompareTag("Player"))
        {
            // Record the attempt
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerDied();
            }

            if (GameManager.Instance != null && GameManager.Instance.currentLives > 0)
            {
                resetTrigger.RespawnPlayer(collision.gameObject);
            }

            // Restart level - SceneManager.LoadScene("GameOver");

        }

        Debug.Log("Respawning player");
    }

    public void ResetTrap()
    {
        hasKilledPlayer = false;
    }
}
