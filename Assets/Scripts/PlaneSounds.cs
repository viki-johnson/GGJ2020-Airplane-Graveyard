using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSounds : MonoBehaviour
{
    public List<AudioClip> planePartsSounds = new List<AudioClip>();
    public AudioSource source;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandomPartSound()
    {
        source.PlayOneShot(planePartsSounds[Random.Range(0, planePartsSounds.Count)], 1);
    }
}
