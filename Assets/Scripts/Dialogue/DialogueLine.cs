using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName = "MISSION DIRECTOR";

    [TextArea(2, 4)]
    public string subtitleText;

    public AudioClip voiceClip;
}
