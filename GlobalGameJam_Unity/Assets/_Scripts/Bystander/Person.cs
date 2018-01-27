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

    protected new void Start()
    {
        anim = GetComponent<Animator>();
    }

    public new void SetDestination(Vector3 point)
    {
        base.SetDestination(point);
        anim.SetBool(Anim_IsMove, true);
        float move = (Random.Range(0, 1f) <= pctRunning) ? 1f : 0f;
        anim.SetFloat(Anim_Move, move);
    }
}
