using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderArea : MonoBehaviour {

    public Vector3 DestinationArea
    {
        get { return transform.position + radius * Vec2toVec3Circle(); }
    }

    public Vector3 SpawnPosition
    {
        get { return transform.position + outerRadius * Vec2toVec3Circle(); }
    }

    [SerializeField] private float outerRadius = 20f;
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

    private Vector3 Vec2toVec3Circle()
    {
        Vector2 circle = Random.insideUnitCircle;
        return new Vector3(circle.x, 0, circle.y);
    }
}
