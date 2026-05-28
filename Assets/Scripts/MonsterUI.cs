using UnityEngine;
using UnityEngine.AI;

public class MonsterUI : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent agent;

    enum State { Roaming, Hunting, Repelled }
    private State currentState;

    [Header("Roaming")]
    public float roamRadius = 15f;
    private Vector3 roamTarget;

    [Header("Detection")]
    public float detectionDistance = 20f;

    [Header("Darkness")]
    public float huntThreshold = 60f;

    [Header("Light Reaction")]
    public float lightRepelDistance = 10f;
    public float repelDuration = 2f;
    private float repelTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Roaming;
        PickNewRoamPoint();
    }

    void Update()
    {
        if (player != null)
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(player.position);
        }

        if (player == null || DarknessSystem.Instance == null) return;

        float darkness = DarknessSystem.Instance.darknessLevel;
        bool lightOn = LightOrb.Instance.IsLightOn;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        HandleStateTransitions(lightOn, darkness, distanceToPlayer);
        HandleStateBehaviour();


        //LIGHT REPEL
        if (lightOn && distanceToPlayer < lightRepelDistance)
        {
            RunAway();
            return;
        }

        //  HUNT MODE
        if (darkness >= huntThreshold)
        {
            HuntPlayer();
        }
        else
        {
            Roam();
        }
    }

    void HandleStateTransitions(bool lightOn, float darkness, float distance)
    {
        // PRIORITY: Light repel
        if (lightOn && distance < lightRepelDistance)
        {
            currentState = State.Repelled;
            repelTimer = repelDuration;
            return;
        }

        // If currently repelled  wait until timer ends
        if (currentState == State.Repelled)
        {
            repelTimer -= Time.deltaTime;
            if (repelTimer <= 0f)
            {
                currentState = State.Roaming;
                PickNewRoamPoint();
            }
            return;
        }

        // Darkness triggers hunting
        if (darkness >= huntThreshold && distance < detectionDistance)
        {
            currentState = State.Hunting;
        }
        else
        {
            currentState = State.Roaming;
        }
    }

    void HandleStateBehaviour()
    {
        switch (currentState)
        {
            case State.Roaming:
                Roam();
                break;

            case State.Hunting:
                HuntPlayer();
                break;

            case State.Repelled:
                RunAway();
                break;
        }
    }



    void HuntPlayer()
    {
        agent.SetDestination(player.position);
    }

    void Roam()
    {
        if (!agent.pathPending && agent.remainingDistance < 1.5f)
        {
            PickNewRoamPoint();
        }

        //if (Vector3.Distance(transform.position, roamTarget) < 2f)
        //{
        //    PickNewRoamPoint();
        //}

        agent.SetDestination(roamTarget);
    }

    void RunAway()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        Vector3 newPos = transform.position + dir * 10f;

        agent.SetDestination(newPos);
    }

    void PickNewRoamPoint()
    {
        Vector3 randomDir = Random.insideUnitSphere * roamRadius;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, roamRadius, 1))
        {
            roamTarget = hit.position;
        }
    }
}
