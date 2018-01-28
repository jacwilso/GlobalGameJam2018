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

	public List<Vector3> LeftSamples
	{
		get { return leftSamples; }
	}

	public List<Vector3> CenterSamples
	{
		get { return centerSamples; }
	}

	public List<Vector3> RightSamples
	{
		get { return rightSamples; }
	}


	[SerializeField] private BezierSpline left, right, center;

	[SerializeField] private float sampleSize = .1f;
	[SerializeField, Range(1e-10f, 1f)] private float sampleEpsilon = .005f;

	private List<Vector3> leftSamples, rightSamples, centerSamples;

	void Start()
	{
		leftSamples = SampleSpline(left);
		centerSamples = SampleSpline(center);
		rightSamples = SampleSpline(right);
	}

	private List<Vector3> SampleSpline(BezierSpline sp)
	{
		if (sp == null)
			return null;

		float progress = 0f;
		List<Vector3> splineSamples = new List<Vector3>();

		Vector3 prevDirection = Vector3.zero, prevSample = Vector3.zero, sample;
		while (progress < 1f)
		{
			sample = sp.GetPoint(progress);

			if (splineSamples.Count > 1)
			{
				Vector3 direction = (sample - prevSample).normalized;

				if (Vector3.Dot(direction, prevDirection) < 1f - sampleEpsilon)
				{
					splineSamples.Add(sample);

					prevDirection = direction;
				}
			}
			else
			{
				splineSamples.Add(sample);
			}

			progress += sampleSize;
			prevSample = sample;
		}

		splineSamples.Add(sp.GetPoint(1f));

		return splineSamples;
	}
}
