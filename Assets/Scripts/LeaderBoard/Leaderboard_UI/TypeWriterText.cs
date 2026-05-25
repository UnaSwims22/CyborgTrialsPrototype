using UnityEngine;
using TMPro;
using System.Collections;

public class TypeWriterText : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    public IEnumerator AnimateText(string fullText, float speed)
    {
        textUI.text = "";

        foreach (char c in fullText)
        {
            textUI.text += c;

            yield return new WaitForSeconds(speed);
        }
    }
}
