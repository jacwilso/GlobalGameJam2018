﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent), typeof(Collider), typeof(Rigidbody))]
public class Bystander : MonoBehaviour {

    public delegate void TramCollisionDelegate();
    public static TramCollisionDelegate tramCollision;

    [Tooltip("The time between the tram leaving the screen and the bystanders entering onto the screen.")]
    public float waitTime;

    [SerializeField] protected SkinObject skins;
    [SerializeField] protected GameObject skinLocation;
    [SerializeField] protected SoundGroup collisionSound, intermittentSound, movementSound;
    [SerializeField] protected float paradeSpeed;

    protected NavMeshAgent agent;
    private bool wasVisible;
    private Vector3 destination;

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
            if (destination != Vector3.zero)
            {
                transform.position += paradeSpeed * transform.forward;
            }
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
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (collision.gameObject.GetComponent<Tram>())
        {
            TramCollision();
        }
        else if (rb != null && collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            rb.useGravity = true;
        }
    }

    protected virtual void TramCollision()
    {
        if (tramCollision != null)
        {
            tramCollision();
        }
        SoundLibrary.instance.PlayRandom(collisionSound);

		agent.enabled = false;
        GetComponent<Ragdoll>().Settled();
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

    public virtual void SetPath(Vector3 point)
    {
        destination = point;
        transform.LookAt(destination);
    }

    public virtual void StopAgent()
    {
        agent.isStopped = true;
    }
}
