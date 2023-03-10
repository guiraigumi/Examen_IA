using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemy : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Chasing,
        Travelling,
        Attacking
    }

    State currentState;
    NavMeshAgent agent;


    public Transform[] destinationPoints;
    int destinationIndex = 0;


    public Transform player;

    [SerializeField]
    float visionRange;
    [SerializeField]
    private float hitRange;
    [SerializeField]


    void Awake()
    {
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    void Start()
    {
        currentState = State.Patrolling;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Attacking:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        agent.destination = destinationPoints[destinationIndex].position;

        if (Vector3.Distance(transform.position, destinationPoints[destinationIndex].position) < 1)
        {
            if (destinationIndex < destinationPoints.Length)
            {
                destinationIndex += 1;
            }

            if (destinationIndex == destinationPoints.Length)
            {
                destinationIndex = 0;
            }


        }

        if (Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }

    void Chase()
    {
        agent.destination = player.position;

        if (Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }
        if (Vector3.Distance(transform.position, player.position) < hitRange)
        {
            currentState = State.Attacking;
            Debug.Log("Atacando");
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) > hitRange)
        {
            currentState = State.Chasing;
        }
    }

    void OnDrawGizmos()
    {
        {
            foreach (Transform point in destinationPoints)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(point.position, 1);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, visionRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, hitRange);

        }
    }

}



