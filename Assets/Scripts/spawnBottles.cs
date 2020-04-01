using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBottles : MonoBehaviour
{
    public GameObject[] bottle;
    public void spawn(Vector3 p) {
        int t = (int)Random.Range(0,3);
            Debug.Log(p.z +""+p.x);

            GameObject go = Instantiate(bottle[t], Vector3.zero, Quaternion.identity);

            go.transform.parent = this.transform;

            go.transform.localPosition = new Vector3(p.x, 0.5f, p.z);
            // float s = Random.Range(5,6);
            // go.transform.localScale = new Vector3(s,s,s);
            // go.transform.localPosition = new Vector3(p.x, p.y + 0.1f, p.z);
    }
}
