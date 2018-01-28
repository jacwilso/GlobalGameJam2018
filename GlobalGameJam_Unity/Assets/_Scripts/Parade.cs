using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parade : MonoBehaviour {
    [Tooltip ("Quantity for the vehicle refers to spawn rate.")]
    [SerializeField] private BystanderMap[] vehicles;
    [SerializeField] private BystanderMap[] parade;

    private float elapsed;
    private int index;
    private BystanderArea[] areas;

	private void Start () {
        index = Random.Range(0, vehicles.Length);
        areas = GetComponentsInChildren<BystanderArea>();
	}

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= (float)vehicles[index].quantity)
        {
            SpawnVehicle();
        }
    }

    private void SpawnVehicle()
    {
        elapsed = 0;
        int areaIndex = Random.Range(0, 2);
        Bystander b = Instantiate<Bystander>(vehicles[index++].bystander, areas[areaIndex].SpawnArea, Quaternion.identity, areas[areaIndex].transform);
        b.SetPath(areas[areaIndex].DestinationArea);
        index = Mathf.Clamp(index, 0, vehicles.Length - 1);
    }

    public void StartParade ()
    {
        int areaIndex = Random.Range(0, 1);
        for (int i = 0; i < parade.Length; i++)
        {
            Bystander b = Instantiate<Bystander>(parade[i].bystander, areas[areaIndex].SpawnArea, Quaternion.identity, areas[areaIndex].transform);
            b.SetPath(areas[areaIndex].DestinationArea);
        }
    }
}
