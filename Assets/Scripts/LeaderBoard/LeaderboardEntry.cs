
[System.Serializable]
public class LeaderboardEntry 
{
    public float finalScore;
    public int starRank;

    public LeaderboardEntry(float score, int stars)
    {
        this.finalScore = score;
        this.starRank = stars;
    }
}
