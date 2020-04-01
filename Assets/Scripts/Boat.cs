using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Weather weather;
    [SerializeField]
    private Radio radio;
    public float disembarkDistance = 1f;
    [SerializeField]
    private float windDampening = 0.1f;
    [SerializeField]
    private float speed = 1.2f;
    [SerializeField]
    private float speedCap = 8f;
    [SerializeField]
    private List<AudioClip> impactSounds;
    [SerializeField]
    private AudioClip moveSound;
    private AudioSource source;

    public AudioSource source2;
    public AudioClip waterSound;

    private bool wPressed = false;
    private bool aPressed = false;
    private bool dPressed = false;
    private bool sPressed = false;

    private Rigidbody body;

    private float rotationSpeed = 2f;
    private float currentRotation = 0f;
    private float currentSpeed = 0f;

    [SerializeField]
    private bool isControlled = true;

    private Vector3 collisionPoint;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        ResetInputKeys();
        if (!isControlled)
        {
            source.Stop();
        }
        if (!isControlled)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            wPressed = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            aPressed = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dPressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            sPressed = true;
        }

        if (isControlled)
        {
            if (!source.isPlaying)
            {
                PlayMoveSound();
            }

            //if (!source2.isPlaying)
            //{
            //    PlayWaterSound();
            //}
        }
        Debug.Log(collisionPoint);
    }

    private void FixedUpdate()
    {
        if (wPressed)
        {
            currentSpeed += speed;
            if (currentSpeed > speedCap)
            {
                currentSpeed = speedCap;
            }
            body.velocity = transform.forward * currentSpeed;
            body.velocity = new Vector3(body.velocity.x, -2f, body.velocity.z);
        }
        else if (sPressed){
            currentSpeed -= speed;
            if (currentSpeed < -speedCap)
            {
                currentSpeed = -speedCap;
            }
            body.velocity = transform.forward * currentSpeed;
            body.velocity = new Vector3(body.velocity.x, -2f, body.velocity.z);
        }

        if (dPressed)
        {
            currentRotation += rotationSpeed;
            body.rotation = Quaternion.Euler(0, currentRotation, 0);
        }

        if (aPressed)
        {
            currentRotation -= rotationSpeed;
            body.rotation = Quaternion.Euler(0, currentRotation, 0);
        }

        CalculateWindEffectsOnBoat();
    }

    private void ResetInputKeys()
    {
        wPressed = false;
        aPressed = false;
        dPressed = false;
        sPressed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Island" && isControlled)
        {
            if (collision.contactCount > 0)
            {
                collisionPoint = collision.contacts[0].point;
            }
            body.velocity = Vector3.zero;
            currentSpeed = 0;
            SwitchControlToPlayer();
            PlayImpactSound();
            
        }
    }

    private void SwitchControlToPlayer()
    {
        SetControlled(false);
        player.SetActive(true);
        Player playerScript = player.GetComponent<Player>();
        playerScript.SetControlled(true);
        playerScript.canMove = true;
        playerScript.PlaceAtPosition(GetPlayerDisembarkPosition(collisionPoint));
        //Debug.Log(collisionPoint);
        //playerScript.PlaceAtPosition(collisionPoint);
    }
    public void SetControlled(bool controlled)
    {
        isControlled = controlled;
    }

    private Vector3 GetPlayerDisembarkPosition(Vector3 collisionPoint)
    {
        Vector3 boatPosition = transform.position;
        RaycastHit hit;

        //if (Physics.Raycast(boatPosition + transform.forward * disembarkDistance + new Vector3(0, 20, 0), Vector3.down, out hit))
        //{
        //    if (hit.point != null)
        //    {
        //        Vector3 direction = (hit.point - transform.position);
        //        Vector3 newDir = new Vector3(direction.x, 0, direction.z).normalized * 0.15f;
        //        return hit.point;
        //    }
        //}
        Vector3 direction = (collisionPoint - boatPosition);
        Vector3 newDir = new Vector3(direction.x, 0, direction.z).normalized * 1.5f;

        if (Physics.Raycast(boatPosition + newDir + new Vector3(0, 20, 0), Vector3.down, out hit))
        {
            if (hit.point != null)
            {
                return hit.point;
            }
        }

        return transform.position + transform.forward * 5f;
    }

    private void CalculateWindEffectsOnBoat()
    {
        if (isControlled)
        {
            float windDegrees = weather.GetWindDegrees();
            Vector3 windDirection = Quaternion.Euler(0, windDegrees, 0) * Vector3.forward;
            body.velocity += weather.GetWindSpeed() * windDampening * windDirection;
        }
    }

    private void PlayImpactSound()
    {
        source.PlayOneShot(impactSounds[Random.Range(0, impactSounds.Count)], 1);
    }

    private void PlayMoveSound()
    {
        source.PlayOneShot(moveSound, 0.3f);
    }

    private void PlayWaterSound()
    {
        source.PlayOneShot(waterSound, 0.2f);
    }
}

