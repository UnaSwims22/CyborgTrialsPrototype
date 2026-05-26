using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.HID;
using UnityEngine.Pool;
using StarterAssets;

public class Keypad : MonoBehaviour
{
    public GameObject playerController;
    public GameObject keypadUI;

    public Animator doorAnimator;

    public TMP_Text displayText;
    public string password; // = "12345";

    public AudioSource buttonSound;
    public AudioSource correctSound;
    public AudioSource wrongSound;

    public Doors linkedDoor;

    private bool isUnlocked = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keypadUI.SetActive(false);
        Debug.Log("Keypad script started");

        if (ProtocolManager.Instance != null && ClueInventorySystem.Instance != null)
        {
            password = GeneratePassword(ClueInventorySystem.Instance.GetCollectedClueAnswers());
            Debug.Log("Generated Password: " + password);
        }
        else
        {
            Debug.LogError("ProtocolManager.Instance or ClueInventorySystem.Instance is null. Cannot generate password.");
            password = "ERROR"; // Fallback
        }

    }

    private void Update()
    {
        if (keypadUI.activeSelf)
        {

            playerController.GetComponent<ThirdPersonController>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

       
    }

    public void Number(int number)
    {
        displayText.text += number.ToString();

        if (buttonSound != null)
            buttonSound.Play();

        Debug.Log("Pressed: " + number);
    }

    public void Execute()
    {
        password = GeneratePassword(
         ClueInventorySystem.Instance.GetCollectedClueAnswers()
        );

        if (displayText.text == password)
        {
            displayText.text = "OPEN";
            isUnlocked = true;

            if (correctSound != null)
                correctSound.Play();

            if (linkedDoor != null)
            {
                linkedDoor.OpenDoor();
            }
            else
            {
                Debug.LogWarning("Linked Door is not assigned on Keypad.");
            }

            if (GameSessionTracker.Instance != null)
            {
                GameSessionTracker.Instance.SuccessfulKeypadInput();
            }

            //linkedDoor.OpenDoor();

           // GameSessionTracker.Instance.SuccessfulKeypadInput();
            //GameSessionTracker.Instance.successfulKeypadInputs++;

        }
        else
        {
            displayText.text = "ERROR";

            if (wrongSound != null)
                wrongSound.Play();

           
            if (GameSessionTracker.Instance != null)
            {
                GameSessionTracker.Instance.IncorrectKeypadInput();
                GameSessionTracker.Instance.incorrectKeypadInputs++;
            }

        }

       
    }

    public void Clear()
    {
        displayText.text = "";

        if (buttonSound != null)
            buttonSound.Play();
    }

    public void Exit()
    {
        keypadUI.SetActive(false);
       

        playerController.GetComponent<ThirdPersonController>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Modified to accept clue answers from ClueInventorySystem
    string GeneratePassword(int[] clueAnswers)
    {
        string result = "";

        foreach (int answer in clueAnswers)
        {
            int modified = answer;

            switch (ProtocolManager.Instance.currentProtocol)
            {
                case ProtocolManager.ProtocolType.AddTwo:
                    modified += 2;
                    break;
                case ProtocolManager.ProtocolType.MultiplyTwo:
                    modified *= 2;
                    break;
                    // Standard protocol does not modify the digit
            }

            result += modified.ToString();
        }

        if (ProtocolManager.Instance.currentProtocol ==
            ProtocolManager.ProtocolType.ReverseOrder)
        {
            char[] chars = result.ToCharArray();
            System.Array.Reverse(chars);
            result = new string(chars);
        }

        return result;
    }
}
