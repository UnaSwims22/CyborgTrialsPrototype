using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Lives ")]
    public int maxLives = 3;
    public int currentLives;

    [Header("Player Reference")]
    public GameObject player;
    public Transform currentCheckpoint;

    [SerializeField] private ResetTrigger resetTrigger;

    private bool isDead = false;
    private bool isInvulnerable = false;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //  void Update()
    //  {
    //    ScoreManager.Instance.livesLost++;
    //  GameSessionTracker.Instance.LoseLife();
    //}

    public void Start()
    {
        currentLives = maxLives;
        UIManager.Instance?.UpdateHearts(currentLives);
    }

    public void PlayerDied()
    {
        if (isDead  || isInvulnerable) return; //  prevents life drain spam
        isDead = true;

        currentLives--;
       // GameSessionTracker.Instance.LoseLife();
        if (GameSessionTracker.Instance != null)
        {
            GameSessionTracker.Instance.LoseLife();
        }
        else
        {
            Debug.LogError("GameSessionTracker is NULL!");
        }

        UIManager.Instance?.UpdateHearts(currentLives);

        CameraShake.Instance?.Shake(2f, 2f, 0.2f);
        Time.timeScale = 0.2f;
        StartCoroutine(ResetTime());



        if (currentLives <= 0)
        {
            
            StartCoroutine(GameOverSequence()); // Tv effect only
        }
        else
        {
            
            DamageFlash.Instance?.Flash();
           
        }

        //  Debug checks
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHearts(currentLives);
        else
            Debug.LogError("UIManager is NULL!");

        if (DamageFlash.Instance != null)
        {
            DamageFlash.Instance.Flash();

        }
        else
            Debug.LogWarning("DamageFlash is NULL");

        StartCoroutine(ResetDeathState());
        Debug.Log("Lives left: " + currentLives);

    }

    IEnumerator ResetTime()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }

    IEnumerator ResetDeathState ()
    {
        yield return new WaitForSeconds(0.5f);

        isDead = false;
        isInvulnerable = false;
    }
  

    public bool IsDead()
    {
        return isDead;
    }


    IEnumerator GameOverSequence()
    {
        if (player != null)
            player.SetActive(false);

        yield return new WaitForSecondsRealtime(0.15f);

        //  TV OFF effect
        if (ScreenFade.Instance != null)
            yield return StartCoroutine(ScreenFade.Instance.TVOff());

        yield return new WaitForSeconds(0.5f);

        if (!GameSessionTracker.Instance.levelCompleted)
        {
            GameSessionTracker.Instance.CalculateResults();

            LeaderboardManager.Instance.AddEntry(
                GameSessionTracker.Instance.finalWeightedScore,
                GameSessionTracker.Instance.starRank
            );
        }


       // GameSessionTracker.Instance.CalculateResults();

        //LeaderboardManager.Instance.AddEntry(
           // GameSessionTracker.Instance.finalWeightedScore,
           // GameSessionTracker.Instance.finalRank
       // );


        SceneManager.LoadScene("GameOver");
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        // Ignore if this is already the active checkpoint
        if (currentCheckpoint == checkpoint) return;

        currentCheckpoint = checkpoint;
        Debug.Log("Checkpoint saved: " + checkpoint.name + " @ " + checkpoint.position);

    }

}
 
    

