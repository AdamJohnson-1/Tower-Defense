using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class EnemyPathfinding : MonoBehaviour
{
    public Transform destination;
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
