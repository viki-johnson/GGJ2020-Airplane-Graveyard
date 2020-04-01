using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTrees : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tree;
    
    public void spawn(Vector3 p)
    {
        if (Random.Range(0, 5) > 3)
        {

            GameObject go = Instantiate(tree, Vector3.zero, Quaternion.identity);
            go.transform.parent = this.transform;
            float s = Random.Range(5,6);
            go.transform.localScale = new Vector3(s,s,s);
            go.transform.localPosition = new Vector3(p.x, p.y + 0.1f, p.z);
        }
    }
}
