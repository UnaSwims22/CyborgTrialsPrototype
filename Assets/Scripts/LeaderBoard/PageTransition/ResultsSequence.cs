using UnityEngine;
using System.Collections;

public class ResultsSequence : MonoBehaviour
{
    //public UIPanelMorph mainPanel;

    public RingGraphUI[] graphs;

    public float[] graphValues;

    public ScoreCounter scoreCounter;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

       // yield return StartCoroutine(
          //  mainPanel.Morph()
        //);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < graphs.Length; i++)
        {
            yield return StartCoroutine(
                graphs[i].AnimateGraph(
                    graphValues[i]
                )
            );

            yield return new WaitForSeconds(
                0.25f
            );
        }

        //foreach (var graph in graphs)
        //{
           // yield return StartCoroutine(
             //    graph.AnimateGraph(
              //       graphValues[i]
              //   )
            //);

            //yield return new WaitForSeconds(
            //    0.25f
            //);
       // }

        yield return StartCoroutine(
            scoreCounter.CountTo(96)
        );
    }
}
