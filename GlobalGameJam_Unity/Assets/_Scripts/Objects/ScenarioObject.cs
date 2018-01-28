using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario")]
public class ScenarioObject : ScriptableObject {
    [Tooltip ("The time between when the tram exits the screen and the bystanders wait time starts. [could be 0]")]
    public float bystanderTime;
    [Tooltip("The time between when the bystanders arrive and when the next tram comes.")]
    public float tramTime;
    public BystanderMap[] leftBystanders, rightBystanders;
    public bool startParade;

    private Parade parade;

    private void OnEnable()
    {
        parade = FindObjectOfType<Parade>();
    }

    public void Spawn(LeverState state, BystanderManager parent)
    {
        BystanderArea area = parent.StateArea(state);
        BystanderMap[] bystanders = (state == LeverState.Left) ? rightBystanders : leftBystanders;
        for (int i = 0; i < bystanders.Length; i++)
        {
            SpawnBystanders(bystanders[i], area);
        }
        if (startParade)
        {
            parade.StartParade();
        }
    }

    public void SpawnBystanders(BystanderMap bystander, BystanderArea parent)
    {
        if (bystander.bystander == null)
        {
            Debug.LogWarning("Empty scenario audience");
            return;
        }
        for (int i = 0; i < bystander.quantity; i++)
        {
            Bystander by = Instantiate<Bystander>(bystander.bystander, parent.SpawnArea, Quaternion.identity, parent.transform);
            by.SetDestination(parent.DestinationArea);
        }
    }
}
