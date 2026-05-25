using System.Collections.Generic;
using UnityEngine;

public class SessionResults : MonoBehaviour
{
    public static SessionResults Instance;

    [HideInInspector] public float lightEfficiency;
    [HideInInspector] public float clueAccuracy;
    [HideInInspector] public float survivorScore;
    [HideInInspector] public float finalScore;

    [HideInInspector] public int starRank;

    [HideInInspector]
    public List<string> earnedMedals =
       new List<string>();


    private void Awake()
    {
        Instance = this;
    }

    public void CalculateResults()
    {
        GameSessionTracker tracker = GameSessionTracker.Instance;

        earnedMedals.Clear();

        // AUTO FAIL CONDITION
        if (!tracker.levelCompleted)
        {
            finalScore = 0;
            starRank = 1;
            lightEfficiency = 0;
            clueAccuracy = 0;
            survivorScore = 0;

            return;
        }


        // LIGHT EFFICIENCY
        lightEfficiency =
        100
        - (tracker.batteryDrainEvents * 35)
        - (tracker.batteryPowerUpsUsed * 10);

        lightEfficiency =
            Mathf.Clamp(lightEfficiency, 0, 100);


        //lightEfficiency =
        //  100
        //  - (tracker.timesBatteryHitZero * 35)
        //  - (tracker.batteryPickupsUsed * 10);

        // lightEfficiency =
        //    Mathf.Clamp(lightEfficiency, 0, 100);

        // int totalLightMistakes =
        //    tracker.batteryPowerUpsUsed +
        //    tracker.batteryDrainEvents;

        // lightEfficiency = Mathf.Clamp(
        //   100 - (totalLightMistakes * 10),
        //    0,
        //   100
        // );



        // CLUE ACCURACY
        int totalAttempts =
        tracker.successfulKeypadInputs +
        tracker.incorrectKeypadInputs;

        // If player never interacted with keypads,
        // clue accuracy should be 0 NOT 100
        if (totalAttempts <= 0)
        {
            clueAccuracy = 0;
        }
        else
        {
            clueAccuracy =
                ((float)tracker.successfulKeypadInputs
                / totalAttempts) * 100f;
        }

        // clueAccuracy = Mathf.Clamp(
        //   100 - (tracker.incorrectKeypadInputs * 15),
        //   0,
        //  100
        // );

        // SURVIVOR SCORE
        switch (tracker.livesLost)
        {
            case 0:
                survivorScore = 100;
                break;

            case 1:
                survivorScore = 70;
                break;

            case 2:
                survivorScore = 35;
                break;

            default:
                survivorScore = 0;
                break;
        }


        // survivorScore = Mathf.Clamp(
        //  100 - (tracker.livesLost * 25),
        //  0,
        // 100
        //);



        // WEIGHTED SCORE
        finalScore =
       (survivorScore * 0.4f)
       + (clueAccuracy * 0.35f)
       + (lightEfficiency * 0.25f);
        // finalScore =
        // (lightEfficiency * 0.4f) +
        //(clueAccuracy * 0.3f) +
        //(survivorScore * 0.3f);



        // FINAL RANK
        if (finalScore >= 95)
            starRank = 5;

        else if (finalScore >= 85)
            starRank = 4;

        else if (finalScore >= 70)
            starRank = 3;

        else if (finalScore >= 50)
            starRank = 2;

        else
            starRank = 1;

        //if (finalScore >= 90)
        // finalRank = "A";
        // else if (finalScore >= 80)
        //   finalRank = "B";
        // else if (finalScore >= 70)
        //   finalRank = "C";
        // else if (finalScore >= 60)
        //  finalRank = "D";
        //else
        // finalRank = "F";



        // MEDALS
        if (lightEfficiency >= 90)
        {
            earnedMedals.Add("Battery Master");
        }

        if (clueAccuracy >= 100
            && tracker.successfulKeypadInputs > 0)
        {
            earnedMedals.Add("Perfect Detective");
        }

        if (tracker.livesLost == 0)
        {
            earnedMedals.Add("Untouched Survivor");
        }
    }
}

