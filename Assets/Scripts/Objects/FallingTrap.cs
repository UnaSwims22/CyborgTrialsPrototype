using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    public Rigidbody fallingObject;
    //public AudioSource warningSound;
    public float dropDelay = 1.0f;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

          //  if(warningSound != null)
            {
              //  warningSound.Play();
            }

            Invoke(nameof(DropObject), dropDelay);

            GetComponent<Collider>().enabled = false;

        }
    }

    public void DropObject()
    {
       if (fallingObject != null)
        {
            fallingObject.isKinematic = false;
            Debug.Log("Player is crushed");
        }
        
    }

}
