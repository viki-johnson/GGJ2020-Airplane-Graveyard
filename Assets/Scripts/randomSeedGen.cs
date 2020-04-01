using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSeedGen : MonoBehaviour
{
    // Start is called before the first frame update

    public float rand;
    public LandscapeMaker[] _land;
    void Start()
    {
        rand = Random.Range(0,100000);

        for(int q=0; q<_land.Length; q++){
            _land[q].r = rand;
            _land[q].create();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
