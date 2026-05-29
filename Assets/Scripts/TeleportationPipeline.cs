using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TeleportationPipeline : MonoBehaviour
{
    [Header("Scene")]
    public string targetSceneName = "TransitionScene";

    [Header("Player")]
    public GameObject player;

    [Header("Portal")]
    public DynamicTeleportationEffect portalEffect;

    [Header("Activation")]
    public float teleportDelay = 2f;

    private bool playerInside = false;

    private bool teleporting = false;

    
    public KeyCode activationKey = KeyCode.Return; // Default to Enter key
   // public GameObject particleEffectPrefab;
   // private Transform teleportationSpawnPoint;
   // private GameObject currentParticleEffect;

    public bool isActivated = false;

    //public DynamicTeleportationEffect dynamicEffectController;


    private void Update()
    {
        if (!playerInside) return;

        if (teleporting) return;

        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartCoroutine(TeleportRoutine());
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = true;

        Debug.Log("Player Entered Portal");

        if (portalEffect != null)
        {
            portalEffect.OnPlayerEnterZone();
        }

       //if (other.gameObject == player)
       //{
         //   playerInZone = true;
         //   Debug.Log("Player entered teleportation zone. Press " + activationKey.ToString() + " to activate.");
          //  if (dynamicEffectController != null) dynamicEffectController.OnPlayerEnterZone(); 
       // }

        //if (other.CompareTag("Player"))
       // {
         //   playerInZone = true;

         //   Debug.Log("PLAYER ENTERED TELEPORT ZONE");

           // if (dynamicEffectController != null)
           // {
           //     dynamicEffectController.OnPlayerEnterZone();
            //}
        //}

    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;

        Debug.Log("Player Exited Portal");

        if (portalEffect != null)
        {
            portalEffect.OnPlayerExitZone();
        }

       // if (other.gameObject == player)
        //{
         //   playerInZone = false;
         //   Debug.Log("Player exited teleportation zone.");
         //   if (dynamicEffectController != null) dynamicEffectController.OnPlayerExitZone(); 
      //  }
    }

    //void Update()
    //{
      //  if (playerInZone && Input.GetKeyDown(activationKey))
      //  {
      //      StartCoroutine(ActivateTeleportation());
      //  }
   // }

  //  private IEnumerator ActivateTeleportation()
    //{
       // Debug.Log("Teleportation activated!");

       // if (dynamicEffectController != null) dynamicEffectController.OnActivationKeyPressed();

        // Play particle effect
      //  if (particleEffectPrefab != null && player != null)
      //  {
       //     currentParticleEffect = Instantiate(particleEffectPrefab, player.transform.position, Quaternion.identity);
            // Optionally, parent the effect to the player or the pipeline for better control
            // currentParticleEffect.transform.parent = player.transform;
       // }

        // Wait for a short duration for animation/particle effects to play
       // yield return new WaitForSeconds(2f); // Adjust this duration as needed

        // Destroy particle effect if it's not self-destroying
       // if (currentParticleEffect != null)
       // {
       //     Destroy(currentParticleEffect);
       // }

        // Teleport player or load new scene
       // if (!string.IsNullOrEmpty(targetSceneName))
        //{
        //    SceneManager.LoadScene(targetSceneName);
       // }


      //  else if (teleportationSpawnPoint != null && player != null)
      //  {
      //      player.transform.position = teleportationSpawnPoint.position;
      //      Debug.Log("Player teleported to new spawn point.");
      //  }
      //  else
      //  {
       //     Debug.LogWarning("Teleportation target not set. Please specify a target scene or a spawn point.");
       // }
   // }

    private IEnumerator TeleportRoutine()
    {
        teleporting = true;

        Debug.Log("Teleportation Activated");

        if (portalEffect != null)
        {
            portalEffect.OnTeleportActivated();
        }

        yield return new WaitForSeconds(teleportDelay);

        SceneManager.LoadScene(targetSceneName);
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
