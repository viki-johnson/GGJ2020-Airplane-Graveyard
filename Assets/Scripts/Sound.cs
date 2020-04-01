using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    List<AudioClip> sound = new List<AudioClip>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void StopAllSounds()
    {
        audioSource.Stop();
    }

    public void PlaySound(int index, float volume)
    {
        audioSource.PlayOneShot(sound[Random.Range(0, sound.Count)], volume);
    }
}
