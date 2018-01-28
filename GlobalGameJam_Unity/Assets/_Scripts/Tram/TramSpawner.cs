using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramSpawner : MonoBehaviour {
    public static TramSpawner instance;

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

    public float PositionDelta
    {
        get { return positionDelta; }
    }

    [SerializeField] private float tramSpeed, uppiness;
	[SerializeField] private Vector2 tramForce;
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
        Spawn();
	}
	
	private void Update () {

	}

    private void OnDeactivate()
    {

    }

    private void Spawn()
    {
        Instantiate(tram, transform.position, transform.rotation, transform);
    }

    private IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(scenarios[scenarioIndex].tramTime);
        Spawn();
        scenarioIndex++;
        if (scenarioIndex >= scenarios.Length)
        {
            scenarioIndex--;
        }
    }

    private IEnumerator WaitBystander()
    {
        yield return new WaitForSeconds(scenarios[scenarioIndex].bystanderTime);
        scenarios[scenarioIndex].Spawn(lastState, bystanderManager);
        StartCoroutine(WaitSpawn());
    }

    public void BringInBystanders()
    {
        StartCoroutine(WaitBystander());
    }
}
