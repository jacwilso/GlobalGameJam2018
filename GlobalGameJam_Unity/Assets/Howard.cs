using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Howard : MonoBehaviour {

    public static Howard instance;

    [SerializeField] private SoundGroup lines;

    private AudioSource audioSrc;
    private int index;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayLine()
    {
        if (index >= lines.soundClips.Length)
        {
            return;
        }
        audioSrc.PlayOneShot(lines.soundClips[index++]);
    }
}
