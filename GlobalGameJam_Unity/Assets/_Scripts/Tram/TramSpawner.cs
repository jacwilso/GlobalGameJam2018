using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramSpawner : MonoBehaviour {
    public static TramSpawner instance;

    public delegate void TramCenter();
    public static TramCenter tramCenter;

    public Transform Intersection
    {
        get { return intersection.transform; }
    }

	public List<Vector3> SplineSamples
	{
		get
		{
			if (lever.State == LeverState.Left)
			{
				return intersection.LeftSamples;
			}
			else if (lever.State == LeverState.Right)
			{
				return intersection.RightSamples;
			}
			else
			{
				return intersection.CenterSamples;
			}
		}
	}

    public Transform Offscreen
    {
        get { return offscreen; }
    }

    public BezierSpline Path
    {
        get
        {
            //return intersection.Center;
            if (lever.State == LeverState.Left)
            {
                return intersection.Left;
            } else if (lever.State == LeverState.Right)
            {
                return intersection.Right;
            } else
            {
                return intersection.Center;
            }
        }
    }

    public LeverState State
    {
        get
        {
            //lastState = LeverState.Center;
            //return LeverState.Center;
            lastState = lever.State;
            return lever.State;
        }
    }

    public float TramSpeed
    {
        get { return tramSpeed; }
    }

    public Vector2 TramForce
    {
        get { return tramForce; }
    }

    public float Uppiness
    {
        get { return uppiness; }
    }

    public float FlipTime
    {
        get { return tramFlipTime; }
    }

    public float FlipSpeed
    {
        get { return tramFlipSpeed; }
    }

    public float PositionDelta
    {
        get { return positionDelta; }
    }

    [SerializeField] private float tramSpeed, uppiness;
	[SerializeField] private Vector2 tramForce;
    [SerializeField] private float tramFlipTime, tramFlipSpeed;
	[SerializeField] private float positionDelta;
    [SerializeField] private GameObject tram;
    [SerializeField] private Transform offscreen;
    [SerializeField] private IntersectionSplineManager intersection;
    [SerializeField] private BystanderManager bystanderManager;
    [SerializeField] private ScenarioObject[] scenarios;

    private LeverController lever;
    private LeverState lastState;
    private int scenarioIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Debug.LogWarning("Have 2 TramSpawners you idiot.");
            Destroy(gameObject);
        }
    }

    private void Start () {
        lever = FindObjectOfType<LeverController>();
	}

    private void OnDisable()
    {
        if (tramCenter != null)
        {
            tramCenter();
        }
    }

    private IEnumerator WaitSpawn()
    {
        if (scenarioIndex % 2 == 1)
        {
            Howard.instance.PlayLine();
        }
        yield return new WaitForSeconds(scenarios[scenarioIndex].tramTime);
        Spawn();
        scenarioIndex++;
    }

    private IEnumerator WaitBystander()
    {
        yield return new WaitForSeconds(scenarios[scenarioIndex].bystanderTime);
        scenarios[scenarioIndex].Spawn(lastState, bystanderManager);
        StartCoroutine(WaitSpawn());
    }

    public void Spawn()
    {
        Instantiate(tram, transform.position, transform.rotation, transform);
    }

    public void BringInBystanders()
    {
        if (scenarioIndex >= scenarios.Length)
        {
            this.enabled = false;
            FindObjectOfType<Parade>().enabled = false;
            return;
        }
        StartCoroutine(WaitBystander());
    }
}
