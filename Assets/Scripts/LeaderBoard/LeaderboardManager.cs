using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    
    public static LeaderboardManager Instance;

    public List<LeaderboardEntry> leaderboard =
        new List<LeaderboardEntry>();

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

    public void AddEntry(float score, int stars)
    {
        leaderboard.Add(new LeaderboardEntry(score, stars));

        leaderboard.Sort((a, b) =>
            b.finalScore.CompareTo(a.finalScore));
    }
}

