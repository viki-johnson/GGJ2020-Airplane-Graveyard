using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{

    public GameObject message;

    private bool isMessageShowing = false;

    private float count = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMessageShowing && count > 0.5f)
        {
            count = 0f;
            message.SetActive(false);
            isMessageShowing = false;
        }

        if (isMessageShowing)
        {
            count += Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetMouseButtonDown(0) && !isMessageShowing)
        {
            isMessageShowing = true;
            ShowMessage();
        }
    }

    private void ShowMessage()
    {
        message.SetActive(true);
    }
}
