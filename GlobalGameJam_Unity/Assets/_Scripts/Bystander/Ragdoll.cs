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

    public void Settled()
    {
        StartCoroutine(SettleCo());
    }

    private IEnumerator SettleCo()
    {
        yield return new WaitForSeconds(Random.Range(8f, 15f));
        EnableRagdoll(false);
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
		{
			collision.gameObject.GetComponent<Rigidbody>().useGravity = true;

			OtherDissolveCo(collision.gameObject);
		}
	}

	private IEnumerator OtherDissolveCo(GameObject go)
	{
		yield return new WaitForSeconds(Random.Range(6f, 12f));
		go.GetComponent<Collider>().enabled = false;
		Destroy(go, 1f);
	}
}

    public void EnableRagdoll(bool isEnabled)
    {
        foreach (Collider rc in ragdollColliders)
        {
            rc.enabled = isEnabled;
        }

        foreach (Rigidbody rr in ragdollRigidbodies)
        {
            rr.isKinematic = !isEnabled;
            rr.useGravity = isEnabled;
        }
    }