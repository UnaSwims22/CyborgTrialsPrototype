using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Note", menuName = "Scriptable Objects/Note")]
public class Note : ScriptableObject
{
    [SerializeField] string label = string.Empty;
    public string Label { get { return label; } }

    [SerializeField] Page[] pages = new Page[0];
    public Page[] Pages { get { return pages; } }
}
