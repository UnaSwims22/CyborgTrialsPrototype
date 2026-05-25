using StarterAssets;
using UnityEngine;

public class OpenKeypad : MonoBehaviour, IInteractable
{
   
    public GameObject keypadUI;
    public GameObject interactText;

    private bool playerInRange = false;

    //[SerializeField] private ThirdPersonController playerController;

    void Start()
    {
        interactText.SetActive(false);
    }

    public void Interact()
    {
       //if (playerInRange)
       // {
         //   keypadUI.SetActive(true);
         //   Debug.Log("Keypad opened");
       // }

        keypadUI.SetActive(true);
        Debug.Log("Keypad opened");

        // playerController.GetComponent<ThirdPersonController>().enabled = false;
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
    }

   // void Update()
   // {
     //   if (playerInRange && Input.GetKeyDown(KeyCode.E))
      //  {
      //      keypadUI.SetActive(true);
       // }
   // }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactText.SetActive(false);
        }
    }
}

