using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class EnemyPathfinding : MonoBehaviour
{
    private static Transform destination;
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        if (destination == null)
        {
            destination = GameObject.FindGameObjectWithTag("Destination").transform;
        }
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
