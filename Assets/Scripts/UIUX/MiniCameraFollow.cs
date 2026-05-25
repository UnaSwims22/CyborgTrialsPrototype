using UnityEngine;

public class MiniCameraFollow : MonoBehaviour
{
    public Transform player;

    //public Transform _mainCamera;

    public Transform cameraTarget;

    public float height = 15f;

    public float rotationSmoothness = 5f;

    // Update is called once per frame
    void LateUpdate()
    {

        // Follow player position (NOT camera position)
        transform.position = player.position + Vector3.up * height;

        // Get rotation from Cinemachine target (mouse-driven)
        float cameraYRotation = cameraTarget.eulerAngles.y;

        Quaternion targetRotation = Quaternion.Euler(90f, cameraYRotation, 0f);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSmoothness
        );
        //transform.position = _mainCamera.position + Vector3.up * height;

        //float cameraYRotation = _mainCamera.eulerAngles.y;
        //Quaternion targetRotation = Quaternion.Euler(90f, cameraYRotation, 0f);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);

    }
}
