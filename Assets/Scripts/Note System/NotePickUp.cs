using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static InteractionSystem;

public class NotePickUp : MonoBehaviour, IInteractable
{
    [SerializeField] Note note = null;

    [SerializeField] bool autoDisplay = false;
    [SerializeField] bool add = true;

    //[Header("Info Content")]
    ///[SerializeField] private string infoTitle = "Point of Interest";
    //[TextArea(3, 6)]
    //[SerializeField] private string infoBody = "Enter your information text here.";


    [Header("Clue Settings")]
    [SerializeField] private bool isClue = false;
    private bool hasTriggered = false;
    [SerializeField] private ClueType clueType = ClueType.None;

    [Header("Trigger Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private TriggerMode triggerMode = TriggerMode.OnEnter;

   // void Update()
    //{
    //    ScoreManager.Instance.cluesCollected++;
    //}

    public void Interact()
    {
       
        
        Debug.Log("Picked up note!");

        if (!hasTriggered)
        {
            switch (clueType)
            {
                case ClueType.Red:
                    InfoUIManager.Instance.AddRedClue();
                    Debug.Log("Collected RED clue");
                    break;

                case ClueType.Blue:
                    InfoUIManager.Instance.AddBlueClue();
                    Debug.Log("Collected BLUE clue");
                    break;

                case ClueType.Green:
                    InfoUIManager.Instance.AddGreenClue();
                    Debug.Log("Collected BLUE clue");
                    break;


            }

            if (isClue)
            {
                InfoUIManager.Instance.AddClue();

                Debug.Log("[Clue] Collected!");
            }

            hasTriggered = true;
        }


        if (autoDisplay)
        {
            NotesSystem.Display(note);
        }
        if (add)
        {
            NotesSystem.AddNote(note.Label, note);
            Destroy(gameObject);
        }
    }

    public enum TriggerMode
    {
        OnEnter,        // Show on enter, hide on exit
        OnStay,         // Only show while actively inside
        EntryAndStay    // Show on enter, keep showing while staying
    }

    public enum ClueType
    {
        None,
        Red,
        Green,
        Blue
    }

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        //if (!hasTriggered)
       // {
           // switch (clueType)
           // {
             //   case ClueType.Red:
               //     InfoUIManager.Instance.AddRedClue();
               //     Debug.Log("Collected RED clue");
                //    break;

               // case ClueType.Blue:
                   // InfoUIManager.Instance.AddBlueClue();
                   // Debug.Log("Collected BLUE clue");
                   // break;
           // }

          //  hasTriggered = true;
        //}

      

        if (!other.CompareTag(playerTag)) return;

        playerInside = true;

        if (triggerMode == TriggerMode.OnEnter ||
            triggerMode == TriggerMode.EntryAndStay)
        {
            
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (triggerMode == TriggerMode.OnStay)
        {
            
            //InfoUIManager.Instance.ShowInfo(infoTitle, infoBody);
        }

        
    }


}
