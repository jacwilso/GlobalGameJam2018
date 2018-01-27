using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SplineWalker), typeof(Rigidbody), typeof(BoxCollider))]
public class Tram : MonoBehaviour {

    [SerializeField] private SkinObject skins;
    [SerializeField] private GameObject skinLocation;

    private bool pathSelected, offscreen;
    private SplineWalker walker;

    private void Start()
    {
        walker = GetComponent<SplineWalker>();
        walker.enabled = false;
        skinLocation.GetComponent<Renderer>().material = skins.Skin;
    }

    private void Update () {
		if (!pathSelected && Mathf.Abs(transform.position.x - TramSpawner.instance.Intersection.position.x) < TramSpawner.instance.PositionDelta)
        {
            SelectPath();
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
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = ((Vector3)Random.insideUnitCircle + transform.forward) + TramSpawner.instance.Uppiness * Vector3.up;
            rb.AddForce(TramSpawner.instance.TramForce * transform.forward);
        }
    }

    private void OnBecameInvisible()
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
            this.enabled = false;
            TramSpawner.instance.enabled = false;
        }
    }
}
