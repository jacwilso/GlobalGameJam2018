using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Bystander")]
public class BystanderObject : ScriptableObject {
    public int quantity;
    public Bystander bystander;

    public void Spawn(GameObject parent)
    {
        for (int i = 0; i < quantity; i++)
        {
            Instantiate(bystander, parent.transform);
        }
    }
}