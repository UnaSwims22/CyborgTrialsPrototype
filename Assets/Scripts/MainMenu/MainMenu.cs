using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load the game scene
    public void PlayGame()
    {
        SceneManager.LoadScene("IntroLevel");
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("QUIT GAME");

        Application.Quit();
    }
}
