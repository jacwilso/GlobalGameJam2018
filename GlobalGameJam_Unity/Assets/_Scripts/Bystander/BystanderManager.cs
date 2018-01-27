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
                return right;
            case LeverState.Right:
                return left;
            case LeverState.Center:
                return null;
        }
        return null;
    }
}
