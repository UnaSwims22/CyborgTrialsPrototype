using UnityEngine;

public class ResultsBadgeSpawner : MonoBehaviour
{
    [Header("BADGE OBJECTS")]
    public GameObject batteryBadge;
    public GameObject detectiveBadge;
    public GameObject survivorBadge;

    
   

    private void Start()
    {
        SpawnEarnedBadges();
    }

    void SpawnEarnedBadges()
    {
        GameSessionTracker tracker =
            GameSessionTracker.Instance;

        // BATTERY MASTER
        batteryBadge.SetActive(
           tracker.earnedBatteryMaster
       );

        // PERFECT DETECTIVE
        detectiveBadge.SetActive(
            tracker.earnedPerfectDetective
        );

        // UNTOUCHED SURVIVOR
        survivorBadge.SetActive(
            tracker.earnedUntouchedSurvivor
        );
    }
}



