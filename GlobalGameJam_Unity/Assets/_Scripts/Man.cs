using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour {

    public const string Anim_Swing = "swingTrigger";

    private Animator anim;

	private void Start () {
        anim = GetComponent<Animator>();
	}
	
	public void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(Anim_Swing);
        }
	}
}
