using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    //Tracked metrics
    public float totalLightUsed = 0f;
    public float usefulLightUsed = 0f;

    public int cluesCollected = 0;
    public int wrongCodeAttempts = 0;

    public int livesLost = 0;
    public int timesBatteryDepleted = 0;


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

    //Calculations

    public float GetLightEfficiency()
    {
        if (totalLightUsed == 0) return 0;
        return usefulLightUsed / totalLightUsed;
    }

    public int GetInfoScore()
    {
        return cluesCollected - (wrongCodeAttempts * 2);
    }

    public int GetStabilityScore()
    {
        return 3 - livesLost - timesBatteryDepleted;
    }

    public int GetFinalScore()
    {
        return Mathf.RoundToInt(
            (GetLightEfficiency() * 30) +
            (GetInfoScore() * 25) +
            (GetStabilityScore() * 20)
            );

    }
}
