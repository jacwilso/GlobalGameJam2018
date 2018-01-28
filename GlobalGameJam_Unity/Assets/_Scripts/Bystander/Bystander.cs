using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent), typeof(Collider), typeof(Rigidbody))]
public class Bystander : MonoBehaviour {

    [Tooltip("The time between the tram leaving the screen and the bystanders entering onto the screen.")]
    public float waitTime;

    [SerializeField] private SkinObject skins;
    [SerializeField] private GameObject skinLocation;
    [SerializeField] private SoundGroup soundGroup;

    protected NavMeshAgent agent;
    private bool wasVisible;

    protected virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
        if (skins != null && skinLocation != null)
        {
            skinLocation.GetComponent<Renderer>().material = skins.Skin;
        }
    }

    protected virtual void Update () {
        if (!agent.enabled)
        {
            return;
        }
        float dist = agent.remainingDistance;
        if (//dist != Mathf.Infinity && 
            //agent.pathStatus == NavMeshPathStatus.PathComplete && 
            dist <= TramSpawner.instance.PositionDelta)
        {
            StopAgent();
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Tram>())
        {
            TramCollision();
        }
    }

    protected virtual void TramCollision()
    {
        SoundLibrary.instance.PlayRandom(soundGroup);
		agent.enabled = false;
    }

    public virtual void SetDestination(Vector3 point)
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        agent.enabled = true;
        agent.SetDestination(point);
    }

    public virtual void StopAgent()
    {
        agent.isStopped = true;
    }
}
