using UnityEngine;
using UnityEngine.AI;

public class AIRandomMovement : MonoBehaviour
{
    public float roamRadius = 10f; // AI karakterimiz için yaıçapımızı belirleyelimmm
    public float waitTime = 3f; // bi bekleme süresi ekldeim

    private NavMeshAgent agent;
    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        // AI hedefe ulaştığında
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                SetRandomDestination();
                waitTimer = 0;
            }
        }
    }
 //rastegele hareket fonsiyonumuz
    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}