using UnityEngine;

public class BridgePlank : MonoBehaviour
{
    public Transform player;
    public float maxDip = 0.3f;
    public float influenceDistance = 2f;
    public float smoothSpeed = 5f;

    private Vector3 startPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        float dipAmount = Mathf.Clamp01(1 - (distance / influenceDistance)) * maxDip;
        Vector3 targetPos = startPos - new Vector3(0, dipAmount, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * smoothSpeed);
    }
}
