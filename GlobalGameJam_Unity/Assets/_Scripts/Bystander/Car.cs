﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Bystander {

    private ParticleSystem ps;
    private AudioSource movementAudio;

	[SerializeField]
	private Renderer[] extraSkinLocations;

    protected override void Start()
    {
        base.Start();
        ps = GetComponentInChildren<ParticleSystem>();
		if (skins != null)
		{
			Material s = skins.Skin;
			if (skinLocation) skinLocation.GetComponent<Renderer>().material = s;
			foreach (Renderer r in extraSkinLocations)
			{
				r.material = s;
			}
		}
        movementAudio = GetComponent<AudioSource>();
        movementAudio.clip = movementSound.GetClip();
        movementAudio.Play();
	}

    public override void StopAgent()
    {
        base.StopAgent();
        movementAudio.Stop();
    }

    protected override void TramCollision()
    {
        base.TramCollision();
        ps.Play();


		Material s = Resources.Load<Material>("Wreckage");
		if (skinLocation) skinLocation.GetComponent<Renderer>().material = s;
		foreach (Renderer r in extraSkinLocations)
		{
			r.material = s;
		}
	}

}
