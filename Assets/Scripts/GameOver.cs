using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private string playerTag = "Player";

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        //GameManager.Instance.PlayerDied();

        //if (!other.CompareTag(playerTag)) return;
        // {
        //        if (!other.CompareTag(playerTag)) return;

        //        GameManager.Instance.PlayerDied();



       // SceneManager.LoadScene("GameOver");
        GameSessionTracker.Instance.levelCompleted = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }

    }




    private void OnDrawGizmos()
    {
        Gizmos.color = playerInside
            ? new Color(0f, 1f, 0f, 0.3f)
            : new Color(1f, 1f, 0f, 0.2f);

        Gizmos.matrix = transform.localToWorldMatrix;

        // Draw based on attached collider type
        if (TryGetComponent<BoxCollider>(out var box))
            Gizmos.DrawCube(box.center, box.size);
        else if (TryGetComponent<SphereCollider>(out var sphere))
            Gizmos.DrawSphere(sphere.center, sphere.radius);
    }
}
