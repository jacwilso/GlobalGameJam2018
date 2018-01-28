using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGroup : ScriptableObject {

	public AudioClip[] soundClips;

	public int currentIndex = 0;
}
