using UnityEngine;

public class ClueTrigger : MonoBehaviour
{
    public ClueData clueData;
    public bool destroyAfterPickup = true;

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Press E to collect clue");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            CollectClue();
        }
    }

    void CollectClue()
    {
        ClueManager.Instance.CollectClue(clueData);

        if (destroyAfterPickup)
            Destroy(gameObject);
    }
}


//private void OnTriggerEnter(Collider other)
       // {
           // if (other.CompareTag("Player"))
          //  {
          //
             //   UIManager.Instance.ShowClue(clue); // pass the clue itself
           // }
        //}
    
