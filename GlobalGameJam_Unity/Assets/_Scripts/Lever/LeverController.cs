using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

    public LeverState State
    {
        get { return state; }
    }

    [Tooltip ("The time it takes for the lever to switch states.")]
    [SerializeField] private float leverTime;
    [Tooltip ("The time the lever is in the center zone.")]
    [SerializeField] private float centerTime;

    private float rotationAngle = 15f;
    private LeverState state = LeverState.Right;
    private Coroutine leverCo;

	private void Start () {
        rotationAngle = 360f - Mathf.Abs(transform.eulerAngles.x);
        leverCo = null;

        if (centerTime > leverTime)
        {
            Debug.LogWarning("Hey your center zone is more than the lever time, restricted to match.");
            centerTime = leverTime;
        }
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && leverCo == null)
        {
            Switch();
        }
	}

    private void Switch()
    {
        leverCo = StartCoroutine(SwitchLever());
    }

    private IEnumerator SwitchLever()
    {
        LeverState intoState = state;
        float newAngle = (2 * ((int)state) - 1) * rotationAngle;
        float notCenterTime = (leverTime - centerTime) / 2f;
        bool switched = false;
        float elapsed = 0;
        float pct;
        while (elapsed < leverTime)
        {
            elapsed += Time.deltaTime;
            pct = elapsed / leverTime;
            float angle = Mathf.Lerp(-newAngle, newAngle, pct);
            transform.eulerAngles = Vector3.right * angle;

            // Center State
            if (notCenterTime < pct && pct < centerTime + notCenterTime)
            {
                state = LeverState.Center;
            } else if (!switched)
            {
                switched = true;
                state = (intoState == LeverState.Left)? LeverState.Right : LeverState.Left;
            }
            yield return null;
        }
        leverCo = null;
    }
}
