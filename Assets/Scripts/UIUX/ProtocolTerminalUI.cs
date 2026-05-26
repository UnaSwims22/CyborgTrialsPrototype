using TMPro;
using UnityEngine;

public class ProtocolTerminalUI : MonoBehaviour
{
    public TMP_Text protocolText;

    void Start()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (ProtocolManager.Instance != null)
        {
            protocolText.text = "VORTEX SECURITY SYSTEM\n\nSESSION ENCRYPTION:\n" + ProtocolManager.Instance.GetProtocolDescription();
        }
        else
        {
            protocolText.text = "VORTEX SECURITY SYSTEM\n\nERROR: PROTOCOL MANAGER NOT FOUND";
            Debug.LogError("ProtocolManager.Instance is null. Make sure ProtocolManager is in the scene and initialized.");
        }

        switch (ProtocolManager.Instance.currentProtocol)
        {
            case ProtocolManager.ProtocolType.Standard:
                protocolText.text =
                    "VORTEX SECURITY SYSTEM\n\nSESSION ENCRYPTION:\nSTANDARD";
                break;

            case ProtocolManager.ProtocolType.AddTwo:
                protocolText.text =
                    "VORTEX SECURITY SYSTEM\n\nSESSION ENCRYPTION:\n+2 SHIFT";
                break;

            case ProtocolManager.ProtocolType.ReverseOrder:
                protocolText.text =
                    "VORTEX SECURITY SYSTEM\n\nSESSION ENCRYPTION:\nREVERSE ORDER";
                break;
        }
    }

}
