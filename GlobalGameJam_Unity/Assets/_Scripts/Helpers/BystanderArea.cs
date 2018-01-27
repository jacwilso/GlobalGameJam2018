using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderArea : MonoBehaviour {

    public Vector3 DestinationArea
    {
        get { return transform.position + RectangleVec2toVec3(size); }
    }

    public Vector3 SpawnArea
    {
        get
        {
            if (index >= spawn.Length)
            {
                index = 0;
                for (int i = 0; i < spawn.Length - 1; i++)
                {
                    for (int j = i; j < spawn.Length; j++)
                    {
                        BystanderSpawn tmp = spawn[i];
                        spawn[i] = spawn[j];
                        spawn[j] = tmp;
                    }
                }
            }
            return spawn[index++].SpawnLocation;
        }
    }

    public Color SpawnAreaColor
    {
        get { return spawnAreaColor; }
    }

    [SerializeField] private Vector2 size = new Vector2(5, 1);
    [SerializeField] private Color trackAreaColor = Color.yellow;
    [SerializeField] private Color spawnAreaColor = Color.green;

    private BystanderSpawn[] spawn;
    private static int index;

    private void Start()
    {
        spawn = GetComponentsInChildren<BystanderSpawn>();
        if (spawn.Length == 0)
        {
            Debug.LogError("NO SPAWN AREAS FOR " + gameObject.name);
        }
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
    }

    private Vector3 RectangleVec2toVec3(Vector2 size)
    {
        return new Vector3(Random.Range(0, size.x), 0, Random.Range(0, size.y));
    }
}
