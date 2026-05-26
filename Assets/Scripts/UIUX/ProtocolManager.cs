using UnityEngine;

public class ProtocolManager : MonoBehaviour
{
    public static ProtocolManager Instance;

    [Header("Debug")]
    public bool randomizeProtocol = true;

    [Header("Session")]
    public string protocolName;

    [TextArea]
    public string protocolDescription;

    public enum ProtocolType
    {
        Standard,
        AddTwo,
        ReverseOrder,
        MultiplyTwo
    }

    public ProtocolType currentProtocol;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (randomizeProtocol)
        {
            GenerateRandomProtocol();
        }
       // GenerateRandomProtocol();
    }

    void GenerateRandomProtocol()
    {
        // Exclude Standard for now to ensure a protocol is always applied
        // Adjust Random.Range max value if more protocols are added or if Standard should be included
        currentProtocol = (ProtocolType)Random.Range(1, System.Enum.GetValues(typeof(ProtocolType)).Length);

        Debug.Log("Current Protocol: " + currentProtocol);
    }

    // Method to get a descriptive string for the current protocol
    public string GetProtocolDescription()
    {
        switch (currentProtocol)
        {
            case ProtocolType.Standard:
                return "STANDARD";
            case ProtocolType.AddTwo:
                return "+2 SHIFT";
            case ProtocolType.ReverseOrder:
                return "REVERSE ORDER";
            case ProtocolType.MultiplyTwo:
                return "MULTIPLIER X2";
            default:
                return "UNKNOWN PROTOCOL";
        }
    }
}
