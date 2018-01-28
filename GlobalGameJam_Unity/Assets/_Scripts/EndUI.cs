using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : MonoBehaviour {

    private RectTransform[] children;

	private void Start () {
        children = GetComponentsInChildren<RectTransform>();
        EnableChildren(false);
        TramSpawner.tramCenter += EnableChildren; 
	}

    private void EnableChildren()
    {
        EnableChildren(true);
    }

    private void EnableChildren(bool enabled)
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(enabled);
        }
        gameObject.SetActive(true);
    }
}
