using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Bystander {

    private ParticleSystem ps;

    protected override void Start()
    {
        base.Start();
        ps = GetComponentInChildren<ParticleSystem>();
		Debug.Log (ps);
    }

    protected override void TramCollision()
    {
        base.TramCollision();
		Debug.Log ("HIT");
        ps.Play();
    }

}
