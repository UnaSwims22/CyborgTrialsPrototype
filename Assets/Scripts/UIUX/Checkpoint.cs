using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string playerTag = "Player";
    
    [Tooltip("Automatically assigned when the player enters this trigger.")]
    private bool isActivated = false;

    [SerializeField] ResetTrigger resetTrigger;

   // [SerializeField] ParticleSystem checkpointVFX;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!isActivated)
            {
                resetTrigger.SetSpawnPoint(transform.position);
                Debug.Log("Checkpoint set to: " + transform.position);
                isActivated = true;
            }
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

        // Draw a vertical marker (makes it easier to see in 3D space)
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, Vector3.up * 2f);
    }
}
