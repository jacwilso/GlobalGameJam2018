using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderSpawn : MonoBehaviour {

    public Vector3 SpawnLocation
    {
        get { return CircleVec2toVec3(spawnRadius); }
    }

    [SerializeField] private float spawnRadius = 10f;

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = GetComponentInParent<BystanderArea>().SpawnAreaColor;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, spawnRadius);
    }

    public Vector3 CircleVec2toVec3(float size)
    {
        Vector2 circle = Random.insideUnitCircle;
        return new Vector3(circle.x, 0, circle.y) * size + transform.position;
    }
}
