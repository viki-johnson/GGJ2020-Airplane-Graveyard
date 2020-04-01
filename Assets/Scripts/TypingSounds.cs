using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingSounds : MonoBehaviour
{
    public List<AudioClip> typingSounds = new List<AudioClip>();
    public AudioSource source;

    private bool keepPlayingSound = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keepPlayingSound && !source.isPlaying)
        {
            PlayRandomTypingSound();
        }
    }

    public void SetKeepPlayingSound(bool status)
    {
        keepPlayingSound = status;
    }

    private void PlayRandomTypingSound()
    {
        source.PlayOneShot(typingSounds[Random.Range(0, typingSounds.Count)], 1);
    }
}
