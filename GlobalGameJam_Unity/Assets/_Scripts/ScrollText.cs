using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour {

    [SerializeField] private float scrollSpeed = 0.5f;

    private RectTransform rect;

	private void Start () {
        rect = GetComponent<RectTransform>();
	}

    private void Update()
    {
        rect.localPosition += Vector3.up * scrollSpeed * Time.deltaTime;
    }
}
