using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tram : MonoBehaviour {

    private bool pathSelected, offscreen;
    private SplineWalker walker;
    private LeverState leverState;

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

    private void SelectPath()
    {
        pathSelected = true;
        walker.spline = TramSpawner.instance.Path;
        walker.enabled = true;
        leverState = TramSpawner.instance.State;
        if (leverState == LeverState.Center)
        {
            this.enabled = false;
            TramSpawner.instance.enabled = false;
        }
    }

    private void OffScreen()
    {
        offscreen = false;
        TramSpawner.instance.BringInBystanders();
        Destroy(gameObject, 3f);
    }
}
