using StarterAssets;
using UnityEngine;

public class BouncyBlockCC : MonoBehaviour
{
   
    [Header("Bounce Settings")]
    [SerializeField] private float bounceForce = 15f;

   // [Header("Optional Audio")]
    //[SerializeField] private AudioSource audioSource;
   //[SerializeField] private AudioClip bounceSound;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Make sure the player hit the block
        if (!hit.collider.CompareTag("Player")) return;

        // Get the player's movement script
        ThirdPersonController player = hit.collider.GetComponent<ThirdPersonController>();
        if (player != null)
        {
            // Set vertical velocity to bounceForce
            //player.SetVerticalVelocity(bounceForce);

            // Play sound if assigned
            //if (audioSource && bounceSound)
                //audioSource.PlayOneShot(bounceSound);
        }
    }
}

