using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    [SerializeField]
    private Speech speechScript;

    public bool started = false;
    public GameObject partsNeeded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" && started)
        {
            speechScript.DisplayProximityText();
            partsNeeded.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            // speechScript.HideSpeechBubble();
            partsNeeded.SetActive(false);
        }
    }
}
