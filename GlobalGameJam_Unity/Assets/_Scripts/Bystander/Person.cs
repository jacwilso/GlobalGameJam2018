using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Person : Bystander {

    public static string Anim_IsMove = "isMoving",
        Anim_Move = "movement";

    [Tooltip ("The percent of people running.")]
    [Range(0,1)]
    [SerializeField] private float pctRunning;

    private Animator anim;
    private ParticleSystem bloodExplosion;

    protected override void Start()
    {
        base.Start();
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        bloodExplosion = GetComponentInChildren<ParticleSystem>();
    }

    protected override void TramCollision()
    {
        base.TramCollision();
        bloodExplosion.Play();
    }

    public override void SetDestination(Vector3 point)
    {
        base.SetDestination(point);
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        anim.SetBool(Anim_IsMove, true);
        float move = (Random.Range(0, 1f) <= pctRunning) ? 1f : 0f;
        anim.SetFloat(Anim_Move, move);
    }

    public override void StopAgent()
    {
        base.StopAgent();
        anim.SetBool(Anim_IsMove, false);
    }
}
