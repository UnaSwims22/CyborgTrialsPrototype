using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Doors : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject interactText;
    public AudioSource doorSound;

    public bool playerInRange = false;
    public bool isLocked = false;
    public bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactText.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
       // if (playerInRange && Input.GetKeyDown(KeyCode.E))
        //{
           // ToggleDoor();
        //}
    }


    void ToggleDoor()
    {
        if (isLocked) return;


        isOpen = !isOpen;

        doorAnimator.SetBool("Open", isOpen);

        if (doorSound != null)
            doorSound.Play();
    }

    public void OpenDoor()
    {
        isOpen = true;
        isLocked = false;

        doorAnimator.SetBool("Open", true);

        if (doorSound != null)
            doorSound.Play();
    }

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
