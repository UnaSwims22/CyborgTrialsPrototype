using UnityEngine;

public class TutorialObjectiveTrigger : MonoBehaviour
{
    [Header("Settings")]
    public int requiredObjectiveIndex;

    public bool destroyAfterTrigger = true;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        Debug.Log(
            $"Trigger Entered | Required: {requiredObjectiveIndex} | Current: {TutorialObjectiveManager.Instance.CurrentObjectiveIndex}"
        );

        if (TutorialObjectiveManager.Instance.CurrentObjectiveIndex
            == requiredObjectiveIndex)
        {
            triggered = true;

            Debug.Log(
                $"Objective {requiredObjectiveIndex} Complete"
            );

            TutorialObjectiveManager.Instance.AdvanceObjective();

            if (destroyAfterTrigger)
            {
                Destroy(gameObject);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
   // {
      //  Debug.Log("Something entered trigger: " + other.name);

      //  if (!other.CompareTag("Player"))
      //  {
      //      Debug.Log("Not player");
       //     return;
       // }

       // Debug.Log("Player entered trigger");

       //     "Current Objective = " +
        //    TutorialObjectiveManager.Instance.CurrentObjectiveIndex
      //  );

       // Debug.Log(
       // "Required Objective = " +
       // requiredObjectiveIndex
   // );

      //  if (TutorialObjectiveManager.Instance.CurrentObjectiveIndex
      //      == requiredObjectiveIndex)
       // {
        //    Debug.Log("Objective Completed!");

        //    TutorialObjectiveManager.Instance.AdvanceObjective();
        //}


       // if (triggered) return;

       // if (!other.CompareTag("Player")) return;

       // if (TutorialObjectiveManager.Instance.CurrentObjectiveIndex == requiredObjectiveIndex)
       // {
       //     triggered = true;

        //    Debug.Log("Objective Completed!");

         //   TutorialObjectiveManager.Instance.AdvanceObjective();

          //  if (destroyAfterTrigger)
          //  {
         //       Destroy(gameObject);
          //  }
        //}
    //}


}





        //if (triggered)
         //   return;

       // if (!other.CompareTag("Player"))
         //   return;

       // Debug.Log(

           // $"Player entered objective trigger {requiredObjectiveIndex}"
            //$"Current Objective = {TutorialObjectiveManager.Instance.CurrentObjectiveIndex}"
       // );

       // Debug.Log(
           // $"Required Objective = {requiredObjectiveIndex}"
        //);

//if (TutorialObjectiveManager.Instance.CurrentObjectiveIndex == 
            //requiredObjectiveIndex)

      //  {
         //   triggered = true;

          //  Debug.Log("Objective Completed!");

          //  TutorialObjectiveManager.Instance.AdvanceObjective();

          //  if (destroyAfterTrigger)
            //    Destroy(gameObject);
       // }
           // return;

       

       //Debug.Log("Something entered trigger: " + other.name);

        //if (!other.CompareTag("Player"))
        //{
        //    Debug.Log("Not player");
        //    return;
        //}

       // Debug.Log("Player entered trigger");

        //Debug.Log(
           // "Current Objective = " +
          //  TutorialObjectiveManager.Instance.CurrentObjectiveIndex
       // );

        //Debug.Log(
        //"Required Objective = " +
        //requiredObjectiveIndex
    //);

       // if (TutorialObjectiveManager.Instance.CurrentObjectiveIndex
          //  == requiredObjectiveIndex)
        //{
           // Debug.Log("Objective Completed!");

        //    TutorialObjectiveManager.Instance.AdvanceObjective();
       // }


        //if (triggered) return;

        //if (!other.CompareTag("Player")) return;

        //if (TutorialObjectiveManager.Instance.CurrentObjectiveIndex == requiredObjectiveIndex)
        //{
        //    triggered = true;

        //    Debug.Log("Objective Completed!");

           // TutorialObjectiveManager.Instance.AdvanceObjective();

           // if (destroyAfterTrigger)
          //  {
           //     Destroy(gameObject);
            //}
        //}
   

