using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Ragdoll : MonoBehaviour {

	[HideInInspector]
	public Collider preRagdollCollider;

	[HideInInspector]
	public Collider[] ragdollColliders;

	[HideInInspector]
	public Rigidbody preRagdollRigidbody;

	[HideInInspector]
	public Rigidbody[] ragdollRigidbodies;

	[HideInInspector]
	public Animator animator;

	protected virtual void Start () {
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

    public void Dissolve()
    {
        StartCoroutine(DissolveCo());
    }

    private IEnumerator DissolveCo()
    {
        yield return new WaitForSeconds(Random.Range(8f, 15f));
        for (int i = 0; i < ragdollColliders.Length; i++)
        {
            ragdollColliders[i].enabled = false;
            yield return new WaitForSeconds(Random.Range(0.25f, 0.45f));
        }
        Destroy(gameObject, 1f);
    }
}
