using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorObject : MonoBehaviour {

    [SerializeField] private float size;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, size * Vector3.one);
    }
}
