using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderManager : MonoBehaviour {

    [SerializeField] private BystanderArea left, right;
	
	public BystanderArea StateArea (LeverState state)
    {
        switch(state)
        {
            case LeverState.Left:
                return left;
            case LeverState.Right:
                return right;
            case LeverState.Center:
                return null;
        }
        return null;
    }
}
