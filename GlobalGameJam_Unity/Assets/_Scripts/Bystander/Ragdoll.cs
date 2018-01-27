using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Ragdoll : MonoBehaviour {

	public Collider preRagdollCollider;

	public Collider[] ragdollColliders;

	public Rigidbody preRagdollRigidbody;

	public Rigidbody[] ragdollRigidbodies;

	public Animator animator;

	// Use this for initialization
	void Start () {
		preRagdollCollider = GetComponent<Collider>();
		preRagdollRigidbody = GetComponent<Rigidbody>();

		Collider[] colliders = GetComponentsInChildren<Collider>();
		ragdollColliders = new Collider[colliders.Length - 1];
		Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
		ragdollRigidbodies = new Rigidbody[rigidbodies.Length - 1];

		animator = GetComponent<Animator>();

		int index = 0;
		foreach (Collider c in colliders)
		{
			if (c == preRagdollCollider)
			{
				continue;
			}

			ragdollColliders[index] = c;
			c.enabled = false;
			index++;
		}

		index = 0;
		foreach (Rigidbody r in rigidbodies)
		{
			if (r == preRagdollRigidbody)
			{
				continue;
			}

			ragdollRigidbodies[index] = r;
			r.useGravity = false;
			r.isKinematic = true;
			index++;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.GetComponent<Tram>())
		{
			// This gets called in the Tram.cs OnCollisionEnter function now to avoid ordering issue
			/*preRagdollCollider.enabled = false;
			preRagdollRigidbody.useGravity = false;
			preRagdollRigidbody.isKinematic = true;

			foreach (Collider rc in ragdollColliders)
			{
				rc.enabled = true;
			}

			foreach (Rigidbody rr in ragdollRigidbodies)
			{
				rr.useGravity = true;
				rr.isKinematic = false;
			}

			animator.enabled = false;*/
		}
	}
}
