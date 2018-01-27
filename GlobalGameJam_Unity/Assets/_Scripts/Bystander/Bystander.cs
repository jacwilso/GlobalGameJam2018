using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bystander : MonoBehaviour {

    [Tooltip("The time between the tram leaving the screen and the bystanders entering onto the screen.")]
    public float waitTime;

    protected NavMeshAgent agent;

    protected virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update () {
        float dist = agent.remainingDistance;
        if (agent.enabled &&
            dist != Mathf.Infinity && 
            agent.pathStatus == NavMeshPathStatus.PathComplete && 
            agent.remainingDistance == 0)
        {
            agent.enabled = false;
        }
    }

    public void SetDestination(Vector3 point)
    {
        Debug.Log(transform.position + " " + point);
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        agent.enabled = true;
        agent.SetDestination(point);
    }
}
