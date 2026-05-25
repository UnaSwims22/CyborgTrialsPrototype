using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerUI : MonoBehaviour
{
   
    public TMP_InputField inputField;
    private ClueData currentClue;

    public void SetClue(ClueData clue)
    {
        currentClue = clue;
    }

    //public void SubmitAnswer()
    //{
      //  if (currentClue == null) return;

       // if (currentClue.CheckAnswer(inputField.text))
       // {
       //    Debug.Log("Correct Answer!");
            // Trigger puzzle success here
       // }
       // else
       // {
       //     Debug.Log("Wrong Answer");
       // }
   // }
}

