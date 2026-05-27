using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ProtocolTerminalUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text protocolNameText;
    public TMP_Text protocolDescriptionText;

    //public TMP_Text protocolText;

    void Start()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (ProtocolManager.Instance == null)
        {
            protocolNameText.text =
                "VORTEX SECURITY SYSTEM\n\nERROR: PROTOCOL MANAGER NOT FOUND";

            Debug.LogError("ProtocolManager.Instance is null.");
            return;
        }

        switch (ProtocolManager.Instance.currentProtocol)
        {
            case ProtocolManager.ProtocolType.Standard:

                protocolNameText.text =
                    "VORTEX SECURITY SYSTEM\n\nSESSION ENCRYPTION:\nSTANDARD";

                protocolDescriptionText.text =
                    "All clue outputs remain unchanged.";

                break;

            case ProtocolManager.ProtocolType.AddTwo:

                protocolNameText.text =
                    "VORTEX SECURITY SYSTEM\n\nSESSION ENCRYPTION:\n+2 SHIFT";

                protocolDescriptionText.text =
                    "All clue outputs are increased by 2.";

                break;

            case ProtocolManager.ProtocolType.ReverseOrder:

                protocolNameText.text =
                    "VORTEX SECURITY SYSTEM\n\nSESSION ENCRYPTION:\nREVERSE ORDER";

                protocolDescriptionText.text =
                    "All collected clue outputs must be entered in reverse sequence.";

                break;

            case ProtocolManager.ProtocolType.MultiplyTwo:

                protocolNameText.text =
                    "VORTEX SECURITY SYSTEM\n\nSESSION ENCRYPTION:\nMULTIPLY x2";

                protocolDescriptionText.text =
                    "All clue outputs are doubled.";

                break;
        }

        Debug.Log("UI DISPLAYING PROTOCOL: " +
            ProtocolManager.Instance.currentProtocol);
    }

}
