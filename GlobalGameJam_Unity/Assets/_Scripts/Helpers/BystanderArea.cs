using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderArea : MonoBehaviour {

    public Vector3 DestinationArea
    {
        get { return transform.position + RectangleVec2toVec3(size); }
    }

    public Vector3 SpawnPosition
    {
        get { return transform.position + CirlceVec2toVec3(spawnRadius); }
    }

    [SerializeField] private Vector2 size = new Vector2(5, 1);
    [SerializeField] private float spawnRadius = 20f;
    [SerializeField] private Color trackAreaColor = Color.yellow;
    [SerializeField] private Color spawnAreaColor = Color.green;

    private void Start ()
    {
    }

    private void OnDrawGizmos()
    {
        Vector3[] verts =
        {
            new Vector3(transform.position.x - size.x/2f, transform.position.y, transform.position.z - size.y/2f),
            new Vector3(transform.position.x + size.x/2f, transform.position.y, transform.position.z - size.y/2f),
            new Vector3(transform.position.x + size.x/2f, transform.position.y, transform.position.z + size.y/2f),
            new Vector3(transform.position.x - size.x/2f, transform.position.y, transform.position.z + size.y/2f),
          };
        UnityEditor.Handles.color = trackAreaColor;
        UnityEditor.Handles.DrawSolidRectangleWithOutline(verts, trackAreaColor, trackAreaColor);
        UnityEditor.Handles.color = spawnAreaColor;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, spawnRadius);
    }

    private Vector3 CirlceVec2toVec3(float size)
    {
        Vector2 circle = Random.insideUnitCircle;
        return size * new Vector3(circle.x, 0, circle.y);
    }

    private Vector3 RectangleVec2toVec3(Vector2 size)
    {
        return new Vector3(Random.Range(0, size.x), 0, Random.Range(0, size.y));
    }
}
