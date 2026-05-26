using UnityEngine;
using System.Collections.Generic;

public class ClueInventorySystem : MonoBehaviour
{
    public static ClueInventorySystem Instance;

    // This list will store the answers to the clues collected by the player.
    // These answers will then be used by the Keypad to generate the base code.
    public List<int> collectedClueAnswers = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Calls this method when the player collects a clue
    public void AddClueAnswer(int answer)
    {
        collectedClueAnswers.Add(answer);
        Debug.Log($"Clue answer collected: {answer}. Total clues: {collectedClueAnswers.Count}");
    }

    // Method to retrieve all collected answers in order
    public int[] GetCollectedClueAnswers()
    {
        return collectedClueAnswers.ToArray();
    }

    // Clear clues for a new game session
   public void ClearClues()
    {
        collectedClueAnswers.Clear();
       Debug.Log("Clue inventory cleared.");
    }
}
