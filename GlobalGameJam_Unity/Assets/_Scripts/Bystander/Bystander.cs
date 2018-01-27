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
		
	}

    public void SetDestination(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
