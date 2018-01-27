using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Bystander")]
public class BystanderObject : ScriptableObject {
    public int quantity;
    public Bystander bystander;

    public void Spawn(BystanderArea parent)
    {
        for (int i = 0; i < quantity; i++)
        {
            Bystander by = Instantiate<Bystander>(bystander, parent.SpawnPosition, Quaternion.identity, parent.transform);
            by.SetDestination(parent.DestinationArea);
        }
    }
}