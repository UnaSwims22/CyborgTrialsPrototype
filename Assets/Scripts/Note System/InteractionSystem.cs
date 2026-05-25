//using Unity.Android.Gradle.Manifest;
using UnityEngine;



public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private RaycastData data = null;

    [SerializeField] private Transform viewCamera = null;

    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private LayerMask layersToRaycast = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       data.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("InteractionSystem running");

        // rest of your code...
        

        if (Input.GetKeyDown(KeyCode.E)) // temporary (replace later)
        {
            //Interact(hit.Value);

            if (data.HitTransfrom)
            {
                var component = data.HitTransfrom.GetComponentInParent<IInteractable>();
                if (component != null)
                {
                    component.Interact();
                    Debug.Log("Interacting with: " + data.HitTransfrom.name);
                }



            }

            Debug.Log("E pressed");
            
        }

        if (Time.frameCount % 4 == 0) // wait  4 frames before doing another raycast
        {

            RaycastHit? hit = DoRayCasting();

            if (hit.HasValue)
            {
                if (data.IsThisNewObject(hit.Value.transform))
                {
                    data.UpdateData(hit);
                }

              

            }
            else
            {
                if (data.HitTransfrom)
                {
                    data.Reset();
                }
            }
        }
    }

   //1st person controller perspective
    // private RaycastHit? DoRayCasting ()
    //{
       // Ray ray = new Ray(viewCamera.position, viewCamera.forward);

       // RaycastHit hit;

       // bool hasHit = Physics.Raycast(ray, out hit, interactionDistance, layersToRaycast);

       // if (hasHit)
       /// {
       //     return hit;

       // }
       // return null;

    //}

    
    //3rd person controller perspective
    private RaycastHit? DoRayCasting()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit, interactionDistance, layersToRaycast);

        if (hasHit)
        {
            return hit;
        }

        return null;
    }

   // private void Interact(RaycastHit hit)
    //{
       //Debug.Log("Interacted with: " + hit.transform.name);

        // Example:
       // var interactable = hit.transform.GetComponent<IInteractable>();
    //
       // if (interactable != null)
       // {
       //     interactable.Interact();
       // }
   // }

   // public interface Interactable
    //{
    //    void Interact();
   // }
}
