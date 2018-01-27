using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTram : MonoBehaviour {

    [SerializeField] private SkinObject skins;
    [SerializeField] private GameObject skinLocation;

    private SplineWalker walker;
    private bool pathSelected, offscreen;

    private void Start()
    {
        skinLocation.GetComponent<Renderer>().material = skins.Skin;
        walker = GetComponent<SplineWalker>();
    }

    private void Update()
    {
        if (!pathSelected && Mathf.Abs(transform.position.x - MenuTramSpawner.instance.Intersection.position.x) < MenuTramSpawner.instance.PositionDelta)
        {
            SelectPath();
        }
        else if (!offscreen && Mathf.Abs(transform.position.x - MenuTramSpawner.instance.Offscreen.position.x) < MenuTramSpawner.instance.PositionDelta)
        {
            Offscreen();
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

    private void Offscreen()
    {
        offscreen = true;
        Destroy(gameObject, 1f);
    }

    private void SelectPath()
    {
        pathSelected = true;
        BezierSpline spline = MenuTramSpawner.instance.Path;
        spline.transform.position = transform.position;
        walker.spline = MenuTramSpawner.instance.Path;
        walker.enabled = true;
    }
}
