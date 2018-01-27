using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTram : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private SkinObject skins;
    [SerializeField] private GameObject skinLocation;

    private void Start()
    {
        skinLocation.GetComponent<Renderer>().material = skins.Skin;
    }

    private void Update () {
        transform.position += transform.forward * speed;
	}

    private void OnBecameInvisible()
    {
        Debug.Log("HI");
        Destroy(gameObject, 1f);
    }
}
