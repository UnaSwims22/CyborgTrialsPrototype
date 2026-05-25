using UnityEngine;

public class InfoTrigger_Tut : MonoBehaviour
{
    

    [Header("Info Content")]
    [SerializeField] private string infoTitle = "Point of Interest";
    [TextArea(3, 6)]
    [SerializeField] private string infoBody = "Enter your information text here.";

    [Header("Trigger Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private TriggerMode triggerMode = TriggerMode.OnEnter;

   // [Header("Clue Settings")]
    //[SerializeField] private bool isClue = false;
    //private bool hasTriggered = false;

    //public enum ClueType
    //{
     //   None,
     //   Red,
      //  Blue
   // }

   // [Header("Clue Settings")]
    //[SerializeField] private ClueType clueType = ClueType.None;



    // Controls WHEN the UI shows
    public enum TriggerMode
    {
        OnEnter,        // Show on enter, hide on exit
        OnStay,         // Only show while actively inside
        EntryAndStay    // Show on enter, keep showing while staying
    }

    private bool playerInside = false;

    // -------------------------------------------
    // Called once when the player enters the zone
    // -------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
       // if (!hasTriggered)
        //{
          //  switch (clueType)
           // {
           //     case ClueType.Red:
           ///         InfoUIManager.Instance.AddRedClue();
             //       Debug.Log("Collected RED clue");
             //       break;
             //
              //  case ClueType.Blue:
               //     InfoUIManager_Tut.Instance.AddBlueClue();
               //     Debug.Log("Collected BLUE clue");
                //    break;
           // }

           // hasTriggered = true;
       // }

        if (!other.CompareTag(playerTag)) return;

        playerInside = true;

        if (triggerMode == TriggerMode.OnEnter ||
            triggerMode == TriggerMode.EntryAndStay)
        {
            //InfoUIManager.Instance.ShowInfo(infoTitle, infoBody);
            InfoUIManager_Tut.Instance.ShowInfo(infoTitle, infoBody);
            Debug.Log($"[InfoTrigger] Player ENTERED: {infoTitle}");
        }

        //if (isClue && !hasTriggered)
        //{
        //    InfoUIManager_Tut.Instance.AddClue();
           // hasTriggered = true;

          //  Debug.Log("[Clue] Collected!");
        //}
    }



    // -------------------------------------------
    // Called every frame while player stays inside
    // -------------------------------------------
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (triggerMode == TriggerMode.OnStay)
        {
            // Useful if you only want the UI while the player
            // is actively standing still in the zone
            InfoUIManager_Tut.Instance.ShowInfo(infoTitle, infoBody);
        }

        // You can also do proximity-based logic here:
        // e.g., update the UI if the player is moving closer to a center point
    }

    // -------------------------------------------
    // Called once when the player exits the zone
    // -------------------------------------------
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        playerInside = false;
        InfoUIManager_Tut.Instance.HideInfo();
        Debug.Log($"[InfoTrigger] Player EXITED: {infoTitle}");
    }

    // Visualize the trigger in the editor
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


