using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelProgression : MonoBehaviour
{
    [Tooltip("Automatically assigned when the player enters this trigger.")]
    private bool isActivated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //f (!other.CompareTag("Player")) return;

        //GameManager.Instance.PlayerDied();

        //if (!other.CompareTag(playerTag)) return;
        // {
        //        if (!other.CompareTag(playerTag)) return;

        //        GameManager.Instance.PlayerDied();

        SceneManager.LoadScene("TransitionScene");

        //SceneManager.LoadScene("GameOver");

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }

    }

    private void OnDrawGizmos()
    {
        // Blue color (brighter when active)
        Gizmos.color = isActivated
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
