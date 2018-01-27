﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario")]
public class ScenarioObject : ScriptableObject {
    [Tooltip ("The time between when the tram exits the screen and the bystanders wait time starts. [could be 0]")]
    public float bystanderTime;
    [Tooltip("The time between when the bystanders arrive and when the next tram comes.")]
    public float tramTime;
    public BystanderMap[] leftBystanders, rightBystanders;

    public void Spawn(LeverState state, BystanderManager parent)
    {
        BystanderArea area = parent.StateArea(state);
        BystanderMap[] bystanders = (state == LeverState.Left) ? rightBystanders : leftBystanders;
        for (int i = 0; i < bystanders.Length; i++)
        {
            SpawnBystanders(bystanders[i], area);
        }
    }

    public void SpawnBystanders(BystanderMap bystander, BystanderArea parent)
    {
        for (int i = 0; i < bystander.quantity; i++)
        {
            Bystander by = Instantiate<Bystander>(bystander.bystander, parent.SpawnArea, Quaternion.identity, parent.transform);
            by.SetDestination(parent.DestinationArea);
        }
    }
}
