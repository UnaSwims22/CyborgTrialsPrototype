using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlayerHUD3D : MonoBehaviour
{
    public static PlayerHUD3D Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI batteryText;
    [SerializeField] private Image[] hearts3D;
    [SerializeField] private Image[] bolts3D;

    [Header("Target")]
    [SerializeField] private Transform playerTransform;

    [Header("HUD Follow Settings")]
    [SerializeField] private Vector3 worldOffset = new Vector3(0.6f, 2.2f, 0f);
    [SerializeField] private float smoothSpeed = 8f;

    private Transform playerCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Auto assign player
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("PlayerHUD3D: No Player found.");
            }
        }

        // Auto assign camera
        if (Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }

        RefreshHUD();
    }

    private void LateUpdate()
    {
        if (playerTransform == null || playerCamera == null)
            return;

        FollowPlayer();
        FaceCamera();
    }

    private void FollowPlayer()
    {
        // Position relative to player
        Vector3 targetPosition = playerTransform.position;

        // Camera-relative offset (Division style)
        targetPosition += playerCamera.right * worldOffset.x;
        targetPosition += playerCamera.up * worldOffset.y;
        targetPosition += playerCamera.forward * worldOffset.z;

        // Smooth movement
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );
    }

    private void FaceCamera()
    {
        // Always face camera
        transform.forward = playerCamera.forward;
    }

    public void RefreshHUD()
    {
        if (GameManager.Instance != null)
        {
            UpdateHeartsDisplay(GameManager.Instance.currentLives);
        }

        if (LightOrb.Instance != null)
        {
            UpdateBatteryDisplay(LightOrb.Instance.currentLifetime);
            UpdateBoltsDisplay(LightOrb.Instance.powerUps);
        }
    }

    public void UpdateHeartsDisplay(int currentLives)
    {
        for (int i = 0; i < hearts3D.Length; i++)
        {
            if (hearts3D[i] != null)
            {
                hearts3D[i].enabled = i < currentLives;
            }
        }
    }

    public void UpdateBatteryDisplay(float currentBattery)
    {
        if (batteryText != null)
        {
            batteryText.text = currentBattery.ToString("0") + "%";
        }
    }

    public void UpdateBoltsDisplay(int amount)
    {
        for (int i = 0; i < bolts3D.Length; i++)
        {
            if (bolts3D[i] != null)
            {
                bolts3D[i].enabled = i < amount;
            }
        }
    }

}


