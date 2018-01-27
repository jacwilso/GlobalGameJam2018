using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderArea : MonoBehaviour {

    public float Radius
    {
        get { return radius; }
    }

    [SerializeField] private Color editorColor = Color.yellow;

    private CircleCollider2D circle;
    private float radius;

    private void Start ()
    {
        circle = GetComponent<CircleCollider2D>();
        radius = circle.radius;
        circle.enabled = false;
    }

    private void OnDrawGizmos()
    {
        circle = GetComponent<CircleCollider2D>();
        UnityEditor.Handles.color = editorColor;
        UnityEditor.Handles.DrawWireDisc(circle.transform.position, Vector3.up, circle.radius);
    }
}
