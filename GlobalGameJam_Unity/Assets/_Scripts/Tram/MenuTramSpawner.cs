using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTramSpawner : MonoBehaviour {

    public static MenuTramSpawner instance;

    public float TramSpeed
    {
        get { return tramSpeed; }
    }

    public Transform Intersection
    {
        get { return intersection; }
    }

    public Transform Offscreen
    {
        get { return offscreen; }
    }

    public BezierSpline Path
    {
        get { return (Random.Range(0, 2) == 1 ? leftCurve : rightCurve); }
    }

    public float PositionDelta
    {
        get { return positionDelta; }
    }

    [SerializeField] private float spawnRate;
    [SerializeField] private float tramSpeed;
    [SerializeField] private float positionDelta;
    [SerializeField] private GameObject tram;
    [SerializeField] private Transform intersection, offscreen;
    [SerializeField] private BezierSpline leftCurve, rightCurve;

    private float elapsed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Have 2 TramSpawners you idiot.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Spawn();
    }

    private void Update () {
        elapsed += Time.deltaTime;
		if (elapsed >= spawnRate)
        {
            Spawn();
        }
	}

    private void Spawn()
    {
        elapsed = 0f;
        Instantiate(tram, transform.position, transform.rotation, transform);
    }
}
