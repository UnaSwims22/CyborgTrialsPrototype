using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Life UI")]
    public Image[] hearts;

    [Header("Power Up UI")]
    public Image[] bolts; 

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHearts(int lives)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < lives;
        }
    }

    public void UpdateBolts(int amount)
    {
        for (int i = 0; i < bolts.Length; i++)
        {
            if (bolts[i] == null)
                continue;

            bolts[i].enabled = i < amount;

           // bolts[i].enabled = i < amount;
        }
    }

    //Write a function that makes sure that only one bolt is used up when orb recharges

   // public void ShowClue(ClueData clue)
  //  {
    ///   InfoUIManager.Instance.ShowInfo(
    //   "Clue Found",
     //  clue.riddleText
  // );
    //}
    
}
