using UnityEngine;

public class PortalRingRotate : MonoBehaviour
{
    public Vector3 rotationDirection = new Vector3(0, 90, 0);

    void Update()
    {
        transform.Rotate(rotationDirection * Time.deltaTime);
    }
}
