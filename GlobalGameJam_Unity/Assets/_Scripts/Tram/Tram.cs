using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SplineWalker), typeof(Rigidbody), typeof(BoxCollider))]
public class Tram : MonoBehaviour {

    [SerializeField] private SkinObject skins;
    [SerializeField] private GameObject skinLocation;

    private bool pathSelected, offscreen;
    private SplineWalker walker;
    private Renderer rend;

    private void Start()
    {
        walker = GetComponent<SplineWalker>();
        walker.enabled = false;
        skinLocation.GetComponent<Renderer>().material = skins.Skin;
        rend = GetComponent<Renderer>();
    }

    private void Update () {
		if (!pathSelected && Mathf.Abs(transform.position.x - TramSpawner.instance.Intersection.position.x) < TramSpawner.instance.PositionDelta)
        {
            SelectPath();
        } else if (!offscreen && Mathf.Abs(transform.position.x - TramSpawner.instance.Offscreen.position.x) < TramSpawner.instance.PositionDelta)
        {
            Offscreen();
        } else 
        {
            if (!walker.enabled)
            {
                transform.position += TramSpawner.instance.TramSpeed * transform.forward;
            } else
            {
                walker.enabled = !walker.Complete;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bystander>())
        {
			Vector3 direction = ((Vector3)Random.insideUnitCircle + transform.forward);
			direction.y = Mathf.Abs(direction.y);
			direction += TramSpawner.instance.Uppiness * Vector3.up;

			Ragdoll ragdoll = collision.gameObject.GetComponent<Ragdoll>();

			if (ragdoll.animator) ragdoll.animator.enabled = false;

			if (collision.gameObject.GetComponent<Car>())
			{
				foreach (Collider rc in ragdoll.ragdollColliders)
				{
					rc.transform.SetParent(null);
				}
			}

			// Disable the pre-ragdoll collider and rigidbody
			ragdoll.preRagdollCollider.enabled = false;
			ragdoll.preRagdollRigidbody.useGravity = false;
			ragdoll.preRagdollRigidbody.isKinematic = true;
			ragdoll.preRagdollRigidbody.velocity = Vector3.zero;
			ragdoll.preRagdollRigidbody.angularVelocity = Vector3.zero;

			// Enable every collider in the bystander ragdoll
			foreach (Collider rc in ragdoll.ragdollColliders)
			{
				rc.enabled = true;
			}

			// Set every rigidbody in the bystander ragdoll to the appropriate values and apply a force
			foreach (Rigidbody rr in ragdoll.ragdollRigidbodies)
			{
				rr.isKinematic = false;
				rr.useGravity = true;
				rr.AddForce(Random.Range(TramSpawner.instance.TramForce.x, TramSpawner.instance.TramForce.y) * direction, ForceMode.Impulse);
			}
        }
    }

    private void Offscreen()
    {
        if (!pathSelected)
        {
            return;
        }
        offscreen = true;
        TramSpawner.instance.BringInBystanders();
        Destroy(gameObject, 3f);
    }

    private void SelectPath()
    {
        pathSelected = true;
        BezierSpline spline = TramSpawner.instance.Path;
        spline.transform.position = transform.position;
        walker.spline = TramSpawner.instance.Path;
        walker.enabled = true;
        if (TramSpawner.instance.State == LeverState.Center)
        {
            walker.lookForward = false;
            StartFlip();
            //StartCoroutine(StartFlip());
            this.enabled = false;
            TramSpawner.instance.enabled = false;
        }
    }

    private void StartFlip()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(TramSpawner.instance.FlipSpeed, 0, TramSpawner.instance.FlipSpeed));
    }

    /*private IEnumerator StartFlip()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        Vector3 start = transform.eulerAngles;
        Vector3 end = start + Vector3.up * 90f;
        Vector3 angle = start;
        float elapsed = 0;
        float time = TramSpawner.instance.FlipTime;
        float speed = TramSpawner.instance.FlipSpeed;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            angle.y = Mathf.Lerp(start.y, end.y, elapsed / time);
            angle.z += Time.deltaTime * speed;
            transform.eulerAngles = angle;
            yield return null;
        }
        ps.Stop();
    }*/
}
