using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float fallDelay = 0.5f;
    public float resetDelay = 3f;
    public bool shouldReset = false;

    //public string playertag = "Player";

    private Rigidbody rb;

    private Vector3 startPos;

    private Quaternion startRot;

    private bool isTriggered = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("FallingPlatform requires a Rigidbody");
            enabled = false;
            return;
        }

        startPos = transform.position;
        startRot = transform.rotation;

        rb.isKinematic = true;
        rb.useGravity = true;

    }

    //public void TriggerFall()
    //{
    //  if (isTriggered) return;

    // isTriggered = true;
    // Invoke(nameof(Fall), fallDelay);
    //}

    void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            Invoke(nameof(Fall), fallDelay);
        }
    }

    void Fall()
    {
        rb.isKinematic = false;

        if (shouldReset)
        {
            Invoke(nameof(ResetPlatform), resetDelay);
        }
    }

    void ResetPlatform()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.SetPositionAndRotation(startPos, startRot);

        rb.isKinematic = true;
        isTriggered = false;
    }

    void OnCollisionStay(Collision collision)
    {
        if (isTriggered) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            Invoke(nameof(Fall), fallDelay);
        }


    }
}




    //[ContextMenu("Reset Platforms")]
  // public void ResetPlatform()
   // {
    //    transform.SetPositionAndRotation(startPos, rot);
    //    rb.isKinematic = true;
    //}

    //[ContextMenu("Testing Platforms")]
  // public void TestFall()
  // {
     //   if (rb == null)
      //      rb = GetComponent<Rigidbody>();

      //  rb.isKinematic = false;

    //}
//}
