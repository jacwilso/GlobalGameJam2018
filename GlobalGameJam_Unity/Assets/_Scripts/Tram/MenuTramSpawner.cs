using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTramSpawner : MonoBehaviour {

    [SerializeField] private GameObject tram;
    [SerializeField] private float spawnRate;

    private float elapsed;

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
        Instantiate(tram, transform);
    }
}
