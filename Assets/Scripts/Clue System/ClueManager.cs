using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
  
    public static ClueManager Instance;

    public List<ClueData> collectedClues = new List<ClueData>();

    private void Update()
    {

    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

       // Instance = this;
    }

    public void CollectClue(ClueData clue)
    {
        if (clue == null) return;


        if (!collectedClues.Contains(clue))
        {
            collectedClues.Add(clue);
            clue.isCollected = true;

            Debug.Log("Collected clue: " + clue.clueTitle);
        }

       // if (!clue.isCollected)
       // {
          //  clue.isCollected = true;
           // collectedClues.Add(clue);

            //Debug.Log("Collected clue: " + clue.clueTitle);
       // }
    }


}

