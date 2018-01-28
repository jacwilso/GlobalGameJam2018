using System.Collections;
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
				r.GetComponent<Renderer>().material = s;
			}
		}
        movementAudio = GetComponent<AudioSource>();
        movementAudio.clip = movementSound.GetClip();
	}

    protected override void TramCollision()
    {
        base.TramCollision();
        ps.Play();
        movementAudio.Stop();
    }

}
