using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
    [SerializeField] private float pushCheckDistance = 1.2f;
    [SerializeField] private LayerMask pushLayers;

    private bool isPushing;

    private Animator animator;
    private float pushTimer = 0.2f;
    private float pushTimerCounter;

    //public LayerMask pushLayers;
	public bool canPush;
	[Range(0.5f, 5f)] public float strength = 1.1f;
    private bool hasAnimator;
    private float pushCounter = 0f;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        // if (pushTimerCounter > 0)
        //{
        // pushTimerCounter -= Time.deltaTime;
        // animator.SetBool("isPushing", true);
        // }
        //else
        // {
        //animator.SetBool("isPushing", false);
        //}
        CheckForPushableObject();

        bool isMoving = GetComponent<CharacterController>().velocity.magnitude > 0.1f;

        animator.SetBool("isPushing", true);
    }

    private void CheckForPushableObject()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 1f, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pushCheckDistance, pushLayers))
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            if (body != null && !body.isKinematic)
            {
                isPushing = true;
                return;
            }
        }

        isPushing = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        
        bool isActuallyPushing = false;
		if (!canPush) return;

        if (controller.isGrounded && PushRigidBodies(hit))
        {
            pushCounter = pushTimer;
        }
        //if (PushRigidBodies(hit))
        //{
           // pushCounter = pushTimer; // refresh timer
        //}


        {
			isActuallyPushing = PushRigidBodies(hit);
            Debug.Log("Pushing!");
        }
//if (hasAnimator)
        //{
           // animator.SetBool("isPushing", isActuallyPushing);
       // }



		animator.SetBool("isPushing", isActuallyPushing);
	}

    public bool IsPushing()
    {
        return pushCounter > 0;
    }

    private bool PushRigidBodies(ControllerColliderHit hit)
	{
		
		
		// https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

		// make sure we hit a non kinematic rigidbody
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic) return false;

		// make sure we only push desired layer(s)
		var bodyLayerMask = 1 << body.gameObject.layer;
		if ((bodyLayerMask & pushLayers.value) == 0) return false;

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3f) return false;

		// Calculate push direction from move direction, horizontal motion only
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

		// Apply the push and take strength into account
		body.AddForce(pushDir * strength, ForceMode.Impulse);

		return true;


	}
}