using UnityEngine;

public class GameSessionTracker : MonoBehaviour
{
    public static GameSessionTracker Instance;

    [Header("Light Metrics")]
    public int batteryPowerUpsUsed;
    public int batteryDrainEvents;

    [Header("Clue Metrics")]
    public int incorrectKeypadInputs;
    public int successfulKeypadInputs;

    [Header("Survival Metrics")]
    public int livesLost;

    [Header("SESSION")]
    public float sessionTime;

    [Header("RESULTS")]
    public float lightEfficiency;
    public float clueAccuracy;
    public float survivorScore;
    public float finalWeightedScore;

    [Header("MEDALS")]
    public bool earnedBatteryMaster;
    public bool earnedPerfectDetective;
    public bool earnedUntouchedSurvivor;

    public int starRank;

    public bool levelCompleted;

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

    private void Update()
    {
        sessionTime += Time.deltaTime;
    }

    // LIGHT SYSTEM TRACKING
    public void UsedBatteryPowerUp()
    {
        batteryPowerUpsUsed++;
    }

    public void BatteryDrained()
    {
        batteryDrainEvents++;
    }

    // CLUE TRACKING
    public void IncorrectKeypadInput()
    {
        incorrectKeypadInputs++;
    }

    public void SuccessfulKeypadInput()
    {
        successfulKeypadInputs++;
    }


    // LIFE TRACKING
    public void LoseLife()
    {
        livesLost++;
    }



    // =========================
    // FINAL SCORE CALCULATION
    // =========================

    public void CalculateResults()
    {
        // =========================
        // HARD FAILURE CONDITION
        // =========================

        if (!levelCompleted)
        {
            lightEfficiency = 0;
            clueAccuracy = 0;
            survivorScore = 0;

            finalWeightedScore = 0;
            starRank = 1;

            return;
        }

        // =========================
        // LIGHT EFFICIENCY
        // =========================

        int lightMistakes =
            batteryPowerUpsUsed +
            (batteryDrainEvents * 2);

        lightEfficiency =
            Mathf.Clamp(
                100 - (lightMistakes * 15),
                0,
                100
            );

        // =========================
        // CLUE ACCURACY
        // =========================

        int totalAttempts =
            successfulKeypadInputs +
            incorrectKeypadInputs;

        if (totalAttempts <= 0)
        {
            clueAccuracy = 0;
        }
        else
        {
            clueAccuracy =
                ((float)successfulKeypadInputs /
                totalAttempts) * 100f;
        }

        // =========================
        // SURVIVOR SCORE
        // =========================
        survivorScore =
    Mathf.Clamp(
        100 - (livesLost * 20),
        0,
        100
    );
        //switch (livesLost)
        //{
        //   case 0:
        //     survivorScore = 100;
        //     break;

        // case 1:
        //      survivorScore = 70;
        //     break;

        //  case 2:
        //     survivorScore = 35;
        //    break;

        // default:
        //  survivorScore = 0;
        //  break;
        // }

        // =========================
        // FINAL WEIGHTED SCORE
        // =========================

        finalWeightedScore =
            (survivorScore * 0.35f) +
            (clueAccuracy * 0.35f) +
            (lightEfficiency * 0.30f);

        // =========================
        // FINAL RANK
        // =========================

        if (finalWeightedScore >= 95)
            starRank = 5;
        else if (finalWeightedScore >= 80)
            starRank = 4;
        else if (finalWeightedScore >= 65)
            starRank = 3;
        else if (finalWeightedScore >= 50)
            starRank = 2;
        else
            starRank = 1;

        // =========================
        // MEDALS
        // =========================

        earnedBatteryMaster =
            lightEfficiency >= 90;

        earnedPerfectDetective =
            clueAccuracy >= 100 &&
            successfulKeypadInputs > 0;

        earnedUntouchedSurvivor =
            livesLost == 0;

        // DEBUG
        Debug.Log("===== SESSION RESULTS =====");
        Debug.Log("Level Completed: " + levelCompleted);

        Debug.Log("Battery Powerups Used: " + batteryPowerUpsUsed);
        Debug.Log("Battery Drains: " + batteryDrainEvents);

        Debug.Log("Successful Inputs: " + successfulKeypadInputs);
        Debug.Log("Incorrect Inputs: " + incorrectKeypadInputs);

        Debug.Log("Lives Lost: " + livesLost);

        Debug.Log("Light Efficiency: " + lightEfficiency);
        Debug.Log("Clue Accuracy: " + clueAccuracy);
        Debug.Log("Survivor Score: " + survivorScore);

        Debug.Log("FINAL SCORE: " + finalWeightedScore);
        Debug.Log("FINAL RANK: " + starRank);

        Debug.Log("Battery Master: " + earnedBatteryMaster);
        Debug.Log("Perfect Detective: " + earnedPerfectDetective);
        Debug.Log("Untouched Survivor: " + earnedUntouchedSurvivor);

        // Debug.Log("Final Score: " + finalWeightedScore);
        // Debug.Log("Final Rank: " + finalRank);

        //GameSessionTracker.Instance.levelCompleted = true;

       // GameSessionTracker.Instance.CalculateResults();
    }

   

    // =========================
    // RESET SESSION
    // =========================

    public void ResetSession()
    {
        batteryPowerUpsUsed = 0;
        batteryDrainEvents = 0;

        incorrectKeypadInputs = 0;
        successfulKeypadInputs = 0;

        livesLost = 0;

        sessionTime = 0;

        lightEfficiency = 0;
        clueAccuracy = 0;
        survivorScore = 0;
        finalWeightedScore = 0;

        starRank = 0;

        levelCompleted = false;

        earnedBatteryMaster = false;
        earnedPerfectDetective = false;
        earnedUntouchedSurvivor = false;

    }
}
