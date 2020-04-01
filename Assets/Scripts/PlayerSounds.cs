using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public List<AudioClip> playFootSounds = new List<AudioClip>();
    public AudioSource source;
    public Player playerScript;

    private bool isMoving = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = false;
        if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
        }
        if (!source.isPlaying && isMoving)
        {
            source.PlayOneShot(playFootSounds[Random.Range(0, playFootSounds.Count)], 0.25f);
        }
    }
}
