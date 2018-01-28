using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Bystander {

    private ParticleSystem ps;

    protected override void Start()
    {
        base.Start();
        ps = GetComponent<ParticleSystem>();
    }

    protected override void TramCollision()
    {
        base.TramCollision();
        ps.Play();
    }

}
