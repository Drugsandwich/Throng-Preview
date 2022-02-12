using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AImovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject target;
    private GameObject fleeTarget;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("StrongHold");
        fleeTarget = GameObject.FindGameObjectWithTag("EnemyPortal");
    }

    //start agent and set the destination
    public void StartAgent()
    {
        agent.enabled = true;
        agent.SetDestination(target.transform.position);
    }

    public void AgentFlee()
    {
        agent.enabled = true;
        agent.SetDestination(fleeTarget.transform.position);
    }

    //stop agent
    public void StopAgent()
    {
        agent.enabled = false;
    }
}
