using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour {

    public const string Anim_Swing = "swingTrigger";

    private Animator anim;

	private void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
