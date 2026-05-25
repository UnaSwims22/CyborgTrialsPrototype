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
    public string password = "12345";

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
    }

    private void Update()
    {
        if (keypadUI.activeSelf)
        {

            playerController.GetComponent<ThirdPersonController>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

       // ScoreManager.Instance.wrongCodeAttempts++;
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
        if (displayText.text == password)
        {
            displayText.text = "OPEN";
            isUnlocked = true;

            if (correctSound != null)
                correctSound.Play();

            linkedDoor.OpenDoor();

            GameSessionTracker.Instance.SuccessfulKeypadInput();
            GameSessionTracker.Instance.successfulKeypadInputs++;

        }
        else
        {
            displayText.text = "ERROR";

            if (wrongSound != null)
                wrongSound.Play();

           // GameSessionTracker.Instance.IncorrectKeypadInput();
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

   
   
}
