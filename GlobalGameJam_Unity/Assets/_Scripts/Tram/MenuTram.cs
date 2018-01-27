using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTram : MonoBehaviour {

    [SerializeField] private SkinObject skins;
    [SerializeField] private GameObject skinLocation;

    private SplineWalker walker;
    private bool pathSelected;

    private void Start()
    {
        skinLocation.GetComponent<Renderer>().material = skins.Skin;
        walker = GetComponent<SplineWalker>();
    }

    private void Update()
    {
        if (!pathSelected && Mathf.Abs(transform.position.x - MenuTramSpawner.instance.Intersection.position.x) < TramSpawner.instance.PositionDelta)
        {
            SelectPath();
        }
        else
        {
            if (!walker.enabled)
            {
                transform.position += MenuTramSpawner.instance.TramSpeed * transform.forward;
            }
            else
            {
                walker.enabled = !walker.Complete;
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1f);
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
