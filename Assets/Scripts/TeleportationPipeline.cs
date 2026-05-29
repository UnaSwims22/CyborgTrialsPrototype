using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TeleportationPipeline : MonoBehaviour
{
    public string targetSceneName;
    public GameObject player;
    public KeyCode activationKey = KeyCode.Return; // Default to Enter key
    public GameObject particleEffectPrefab;
    private Transform teleportationSpawnPoint;

    private bool playerInZone = false;
    private GameObject currentParticleEffect;

    public bool isActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInZone = true;
            Debug.Log("Player entered teleportation zone. Press " + activationKey.ToString() + " to activate.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInZone = false;
            Debug.Log("Player exited teleportation zone.");
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(activationKey))
        {
            StartCoroutine(ActivateTeleportation());
        }
    }

    private IEnumerator ActivateTeleportation()
    {
        Debug.Log("Teleportation activated!");

        // Play particle effect
        if (particleEffectPrefab != null && player != null)
        {
            currentParticleEffect = Instantiate(particleEffectPrefab, player.transform.position, Quaternion.identity);
            // Optionally, parent the effect to the player or the pipeline for better control
            // currentParticleEffect.transform.parent = player.transform;
        }

        // Wait for a short duration for animation/particle effects to play
        yield return new WaitForSeconds(2f); // Adjust this duration as needed

        // Destroy particle effect if it's not self-destroying
        if (currentParticleEffect != null)
        {
            Destroy(currentParticleEffect);
        }

        // Teleport player or load new scene
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }


        else if (teleportationSpawnPoint != null && player != null)
        {
            player.transform.position = teleportationSpawnPoint.position;
            Debug.Log("Player teleported to new spawn point.");
        }
        else
        {
            Debug.LogWarning("Teleportation target not set. Please specify a target scene or a spawn point.");
        }
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

    }
}
