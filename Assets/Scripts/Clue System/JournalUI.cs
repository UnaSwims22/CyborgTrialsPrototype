using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class JournalUI : MonoBehaviour
{
  
    public GameObject journalPanel;
    public Transform contentParent;
    public GameObject clueButtonPrefab;

    public TMP_Text detailTitle;
    public TMP_Text detailDescription;

    private void Start()
    {
        journalPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleJournal();
        }
    }

 
    public void ToggleJournal()
    {
        journalPanel.SetActive(!journalPanel.activeSelf);

        if (journalPanel.activeSelf)
        {
            RefreshJournal();
        }
    }

    void RefreshJournal()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ClueData clue in ClueManager.Instance.collectedClues)
        {
            GameObject btn = Instantiate(clueButtonPrefab, contentParent);

            btn.GetComponentInChildren<TMP_Text>().text = clue.clueTitle;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ShowDetails(clue);
            });
        }
    }

    void ShowDetails(ClueData clue)
    {
        detailTitle.text = clue.clueTitle;
        detailDescription.text = clue.clueDescription;
    }
}
//void DisplayClues()
  //  {
    //    foreach (ClueData clue in ClueManager.Instance.collectedClues)
      //  {
        //    Debug.Log(clue.riddleText);
        //}
    //}


