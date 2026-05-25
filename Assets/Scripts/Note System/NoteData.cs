using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class NoteData : MonoBehaviour
{
    [SerializeField] Image bgImage = null;
    [SerializeField] TextMeshProUGUI label = null;
    [SerializeField] Button button = null;

    private Note note = null;
    private RectTransform rect = null;
    public RectTransform Rect
    {
        get
        {
            if (rect == null)
            {
                rect = GetComponent<RectTransform>();
                if (rect == null) { rect = gameObject.AddComponent<RectTransform>(); }
            }
            return rect;
        }
    }

    public void UpdateInfo(Note note, Color color)
    {
        this.note = note;

        label.text = note.Label;
        bgImage.color = color;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Display);
    }
    
    public void Display ()
    {
        //Display the note
        NotesSystem.Display(note);
        Debug.Log("Opening note: " + note.Label);
    }
    
}
