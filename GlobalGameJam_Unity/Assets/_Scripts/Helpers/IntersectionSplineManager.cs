using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionSplineManager : MonoBehaviour {

    public BezierSpline Left
    {
        get { return left; }
    }

    public BezierSpline Right
    {
        get { return right; }
    }

    public BezierSpline Center
    {
        get { return center;}
    }

    [SerializeField] private BezierSpline left, right, center;
}
