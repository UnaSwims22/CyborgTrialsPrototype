using UnityEngine;

public class ClueData : MonoBehaviour
{
    
    [Header("Clue Info")]
    public string clueID;
    public string clueTitle;
    [TextArea(3, 5)]
    public string clueDescription;

    [Header("Optional")]
    public Sprite clueImage;

    [Header("State")]
    public bool isCollected = false;
}
   // public string clueID;
    //[TextArea] public string riddleText;
    //public string correctAnswer;
    //public bool isCollected = false;

    //public bool CheckAnswer(string playerAnswer)
   // {
   //     return playerAnswer.ToLower().Trim() == correctAnswer.ToLower().Trim();
    //}
//}
   
