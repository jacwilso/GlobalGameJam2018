using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

	KeyCode key = KeyCode.V;

	public Camera primaryCamera;
	public Camera secondaryCamera;
	SmoothMouseLook sml;

	bool onSecondaryCamera = false;
	bool _onSecondaryCamera = false;

	[Space]

	public GameObject unloadedObjects;

	void Start ()
	{
		sml = GetComponent<SmoothMouseLook>();
		sml.enabled = onSecondaryCamera;
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(key))
		{
			_onSecondaryCamera = !_onSecondaryCamera;
		}

		if (_onSecondaryCamera != onSecondaryCamera)
		{
			onSecondaryCamera = _onSecondaryCamera;
			primaryCamera.enabled = !onSecondaryCamera;
			secondaryCamera.enabled = onSecondaryCamera;

			sml.enabled = onSecondaryCamera;
			sml.Cutoff();

			if (unloadedObjects)
			{
				unloadedObjects.SetActive(onSecondaryCamera);
			}
		}
	}
}
