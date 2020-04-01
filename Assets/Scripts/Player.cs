using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject boat;
    public Transform modelTransform;

    private bool wPressed = false;
    private bool aPressed = false;
    private bool dPressed = false;
    private bool sPressed = false;

    public CharacterController body;
    public float speed = 2f;
    private float speedCap = 3f;
    private float rotationSpeed = 2f;
    private float currentRotation = 0f;
    private float currentSpeed = 0f;
    private float gravity = -9.81f;

    public bool isControlled = false;

    public bool canMove = false;


    void Start()
    {

    }

    void Update()
    {
        ResetInputKeys();
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
    }

    private void FixedUpdate()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the Direction to Move based on the tranform of the Player
        Vector3 moveDirectionForward = transform.forward * verticalInput;
        Vector3 moveDirectionSide = transform.right * horizontalInput;

        //find the direction
        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        //find the distance
        Vector3 distance = direction * speed;

        // Apply Movement to Player
        if(canMove){
            body.SimpleMove(distance);
        }

        // rotation
        Vector3 NextDir = new Vector3(horizontalInput, 0, verticalInput);
        if (NextDir != Vector3.zero)
            modelTransform.rotation = Quaternion.LookRotation(NextDir);

    }

    private void ResetInputKeys()
    {
        wPressed = false;
        aPressed = false;
        dPressed = false;
        sPressed = false;
    }

    private void SwitchControlToBoat()
    {
        SetControlled(false);
        Boat boatScript = boat.GetComponent<Boat>();
        boatScript.SetControlled(true);
        gameObject.SetActive(false);
    }

    public void SetControlled(bool controlled)
    {
        isControlled = controlled;
    }

    public void PlaceAtPosition(Vector3 position)
    {
        body.enabled = false;
        gameObject.transform.position = position;
        body.enabled = true;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.transform.root.tag == "Boat")
        {
            body.Move(Vector3.zero);
            currentSpeed = 0;
            SwitchControlToBoat();
        }

        if (hit.gameObject.tag == "water")
        {
            Vector3 direction = (transform.position - hit.point);
            Vector3 newDir = new Vector3(direction.x, 0, direction.z).normalized * 0.15f;
            //PlaceAtPosition(transform.position + newDir);
            gameObject.transform.position = transform.position + newDir;
        }
    }

}
