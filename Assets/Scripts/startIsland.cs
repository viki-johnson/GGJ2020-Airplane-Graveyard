using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startIsland : MonoBehaviour
{
    // Start is called before the first frame update

    public LandscapeMaker _lm;

    public Vector3 pos;

    public GameObject island;

    void Start(){
        pos = new Vector3(Random.Range(0,_lm.width*_lm.cellSize/2), 0.5f, Random.Range(0,_lm.height*_lm.cellSize/2));
        island.transform.position = pos;
    }
    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag == "Island"){
            Debug.Log("island");
            pos = new Vector3(_lm.width*_lm.cellSize, 0.5f, _lm.height*_lm.cellSize);
            island.transform.position = pos;
        }
    }
}
