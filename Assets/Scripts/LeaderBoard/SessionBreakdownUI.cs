using UnityEngine;
using TMPro;
using System.Collections;

public class SessionBreakdownUI : MonoBehaviour
{
   //public TextMeshProUGUI lightText;
    //public TextMeshProUGUI clueText;
    //public TextMeshProUGUI survivorText;
    //public TextMeshProUGUI finalScoreText;
   //public TextMeshProUGUI rankText;

    [Header("Graphs")]
    public RingGraphUI lightGraph;
    public RingGraphUI clueGraph;
    public RingGraphUI survivorGraph;

    [Header("Labels")]
    public TypeWriterText lightLabel;
    public TypeWriterText clueLabel;
    public TypeWriterText survivorLabel;


    [Header("Final")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI rankText;

    public StarRankUI starUI;

    private void Start()
    {
        StartCoroutine(PlayBreakdownSequence());
    }

    //private void Start()
    //{
      //  var results = GameSessionTracker.Instance;

       // SessionResults results = SessionResults.Instance;

      //  lightText.text =
         //   "Light Efficiency: " +
         //   results.lightEfficiency.ToString("F0") + "%";

       // clueText.text =
        //    "Clue Accuracy: " +
           // results.clueAccuracy.ToString("F0") + "%";

        //survivorText.text =
           // "Survivor: " +
           // results.survivorScore.ToString("F0") + "%";

       // finalScoreText.text =
           // "Final Score: " +
           // results.finalWeightedScore.ToString("F0");
        
       // rankText.text =
         //   "FINAL RANK: " +
          //  results.finalRank;
    

    IEnumerator PlayBreakdownSequence()
    {
        GameSessionTracker results =
            GameSessionTracker.Instance;

        // LIGHT
        yield return StartCoroutine(
            lightLabel.AnimateText(
                "LIGHT EFFICIENCY",
                0.03f
            )
        );

        yield return StartCoroutine(
            lightGraph.AnimateGraph(
                results.lightEfficiency
            )
        );


        yield return new WaitForSeconds(0.5f);

        // CLUE
        yield return StartCoroutine(
            clueLabel.AnimateText(
                "CLUE ACCURACY",
                0.03f
            )
        );

        yield return StartCoroutine(
            clueGraph.AnimateGraph(
                results.clueAccuracy
            )
        );

        yield return new WaitForSeconds(0.5f);

        // SURVIVOR
        yield return StartCoroutine(
            survivorLabel.AnimateText(
                "SURVIVOR SCORE",
                0.03f
            )
        );

        yield return StartCoroutine(
            survivorGraph.AnimateGraph(
                results.survivorScore
            )
        );

        yield return new WaitForSeconds(1f);

        // FINAL SCORE COUNT UP
        yield return StartCoroutine(
            CountFinalScore(
                results.finalWeightedScore
            )
        );

        yield return StartCoroutine(
            starUI.AnimateStars(
                results.starRank
                )
            );
        
        
        //rankText.text =
           // "RANK : " + results.finalRank;
    }

    IEnumerator CountFinalScore(float target)
    {
        float current = 0;

        while (current < target)
        {
            current += Time.deltaTime * 60f;

            finalScoreText.text =
                " " +
                Mathf.RoundToInt(current);

            yield return null;
        }

        finalScoreText.text =
            " " +
            Mathf.RoundToInt(target);
    }


}


