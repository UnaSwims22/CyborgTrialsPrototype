using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI attemptText;
    public string gameSceneName = "SampleScene";

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            attemptText.text = "You Lost All Lives!";
        }
    }
    void Update()
    {
        // Press ESC to quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("SampleScene");
        }

       // SessionBreakdownUI.SetActive(true);
        //sessionBreakdownUI.GetComponent<SessionBreakdownUI>().ShowResults();

    }

    public void ReplayGame()
    {
       // GameManager.Instance.ResetGame();
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Game has been initiated");
        

        
    }

    public void QuitGame()
    {
        
        Application.Quit();

        // For testing inside Unity
        Debug.Log("Game Closed");
    }
}

