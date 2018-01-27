using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SplineWalker), typeof(Rigidbody), typeof(BoxCollider))]
public class Tram : MonoBehaviour {

    private bool pathSelected, offscreen;
    private SplineWalker walker;

    private void Start()
    {
        walker = GetComponent<SplineWalker>();
        walker.enabled = false;
    }

    private void Update () {
		if (!pathSelected && Mathf.Abs(transform.position.x - TramSpawner.instance.Intersection.position.x) < TramSpawner.instance.PositionDelta)
        {
            SelectPath();
        } else
        {
            if (!offscreen && Mathf.Abs(transform.position.x - TramSpawner.instance.OffScreen.position.x) < TramSpawner.instance.PositionDelta)
            {
                OffScreen();
            }
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
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = ((Vector3)Random.insideUnitCircle + transform.forward);
            rb.AddForce(TramSpawner.instance.TramForce * transform.forward);
        }
    }

    private void SelectPath()
    {
        pathSelected = true;
        walker.spline = TramSpawner.instance.Path;
        walker.enabled = true;
        if (TramSpawner.instance.State == LeverState.Center)
        {
            this.enabled = false;
            TramSpawner.instance.enabled = false;
        }
    }

    private void OffScreen()
    {
        offscreen = true;
        TramSpawner.instance.BringInBystanders();
        Destroy(gameObject, 3f);
    }
}
