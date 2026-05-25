using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class ResetTrigger : MonoBehaviour
{
    public string playertag = "Player";

    public Transform startPos;

    private Vector3 spawnPoint;


    [SerializeField] private CinemachineVirtualCamera vcam;

    public UnityEvent respawn;

    private void Start()
    {
        spawnPoint = startPos.position;
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public void RespawnPlayer(GameObject player)
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.transform.position = spawnPoint;

        if (cc != null)
            cc.enabled = true;

        if (vcam != null)
        {
            vcam.OnTargetObjectWarped(player.transform, Vector3.zero); // spawnPoint - player.transform.position);
        }

        respawn.Invoke();

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("Respawned via ResetTrigger at: " + spawnPoint);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playertag))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerDied();
            }

            //  Only respawn if player still has lives
            if (GameManager.Instance.currentLives > 0)
            {
                RespawnPlayer(other.gameObject);
            }


        }

    }
}
