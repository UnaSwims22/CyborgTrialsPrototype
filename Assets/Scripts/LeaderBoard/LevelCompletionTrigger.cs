using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletionTrigger : MonoBehaviour
{
   
    private bool completed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (completed) return;

        if (other.CompareTag("Player"))
        {
            completed = true;

            GameSessionTracker.Instance.levelCompleted = true;

            GameSessionTracker.Instance.CalculateResults();

            LeaderboardManager.Instance.AddEntry(
                GameSessionTracker.Instance.finalWeightedScore,
                GameSessionTracker.Instance.starRank
            );

            SceneManager.LoadScene("GameOver");
        }
    }

 

    private void OnDrawGizmos()
    {
        // Blue color (brighter when active)
        Gizmos.color = completed
            ? new Color(0f, 0.5f, 1f, 0.5f)   // Active checkpoint (strong blue)
            : new Color(0f, 0.3f, 1f, 0.25f); // Idle checkpoint (faded blue)

        Gizmos.matrix = transform.localToWorldMatrix;

        // Draw based on collider type
        if (TryGetComponent<BoxCollider>(out var box))
            Gizmos.DrawCube(box.center, box.size);
        else if (TryGetComponent<SphereCollider>(out var sphere))
            Gizmos.DrawSphere(sphere.center, sphere.radius);

        // Draw a vertical marker (makes it easier to see in 3D space)
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, Vector3.up * 2f);
    }
}

