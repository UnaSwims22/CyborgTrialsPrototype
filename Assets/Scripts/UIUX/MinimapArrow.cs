using UnityEngine;

public class MinimapArrow : MonoBehaviour
{
    public Transform cameraTransform; //player's minimap camera (player's camera)
    public RectTransform arrowIcon; // ui arrow

    public float rotationSmoothness = 8f;
    
   

    // Update is called once per frame
    void Update()
    {
        float cameraYRotation = cameraTransform.eulerAngles.y;

        Quaternion targetRotation = Quaternion.Euler(0, 0, -cameraYRotation);
        arrowIcon.rotation = Quaternion.Lerp(arrowIcon.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
        Debug.Log(cameraTransform.name);
    }
}
