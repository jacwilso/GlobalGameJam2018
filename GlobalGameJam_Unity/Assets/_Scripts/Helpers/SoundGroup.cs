using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ()]

public class SoundGroup : ScriptableObject {

	public AudioClip[] soundClips;

	public int currentIndex = 0;

    public AudioClip GetClip()
    {
        if (currentIndex >= soundClips.Length)
        {
            currentIndex = 0;
        }
        return soundClips[currentIndex++];
    }
}
